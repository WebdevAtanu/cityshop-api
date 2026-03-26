using cityshop_api.DTO;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace cityshop_api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly ICustomerRepository _customerRepository;
        public OrderRepository(ApplicationDBContext context, ICustomerRepository customerRepository)
        {
            _context = context;
            _customerRepository = customerRepository;
        }

        private async Task<string> GenerateOrderNumber(Guid shopId, string shopCode)
        {
            string? lastOrderNumber = await _context.ShopOrders
                .Where(s => s.ShopId == shopId)
                .OrderByDescending(s => s.OrderNumber)
                .Select(s => s.OrderNumber)
                .FirstOrDefaultAsync();

            int newSeries = 1;

            if (!string.IsNullOrEmpty(lastOrderNumber))
            {
                // Split MT01-000007
                string[] parts = lastOrderNumber.Split('-');

                if (parts.Length == 2)
                {
                    int lastNumber = int.Parse(parts[1]);
                    newSeries = lastNumber + 1;
                }
            }

            return $"{shopCode}-{newSeries:D6}";
        }

        public async Task<string> CreateOrder(OrderRequest orderRequest)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Shop shop = await _context.Shops.Where(s => s.ShopId == orderRequest.ShopId).FirstOrDefaultAsync() ?? throw new Exception("Shop details not found");
                var customer = orderRequest.OrderBy;

                // handle customer
                if (orderRequest.OrderBy == null)
                {
                    throw new Exception("Customer details are required");
                }

                if (orderRequest.OrderedItems == null || !orderRequest.OrderedItems.Any())
                {
                    throw new Exception("No items in order");
                }

                Guid customerId = Guid.NewGuid();
                Guid orderId = Guid.NewGuid();

                _context.Customers.Add(new Customer
                {
                    CustomerId = customerId,
                    CustomerName = customer?.CustomerName,
                    CustomerPhone = customer?.CustomerPhone,
                    CustomerEmail = customer?.CustomerEmail,
                    CustomerAddress = customer?.CustomerAddress,
                    Pincode = customer?.Pincode,
                    CreatedDate = DateTime.Now,
                    DLM = DateTime.Now,
                    IsActive = true,
                });

                string orderNumber = await GenerateOrderNumber(shop.ShopId, shop.ShopCode ?? "SC");

                _context.ShopOrders.Add(new ShopOrder
                {
                    OrderId = orderId,
                    ShopId = shop?.ShopId,
                    OrderNumber = orderNumber,
                    CustomerId = customerId,
                    DeliveryAddress = customer?.CustomerAddress,
                    Pincode = customer?.Pincode,
                    Phone = customer?.CustomerPhone,
                    CreatedDate = DateTime.Now,
                    DLM = DateTime.Now,
                    IsActive = true,
                });

                var allShopItems = await (from i in _context.Items
                                          join si in _context.ShopItems
                                            on i.ItemId equals si.ItemId
                                          where si.ShopId == shop.ShopId
                                          select new
                                          {
                                              i,
                                              si
                                          }).ToListAsync();

                var itemPriceMap = allShopItems.ToDictionary(x => x.si.ItemId, x => x.si.Price);

                if (orderRequest.OrderedItems is not null)
                {
                    foreach (var item in orderRequest.OrderedItems)
                    {
                        _context.OrderItems.Add(new OrderItem
                        {
                            MapId = Guid.NewGuid(),
                            OrderId = orderId,
                            ItemId = item.ItemId,
                            SizeId = item.SizeId,
                            ItemQty = item.Qty,
                            //ItemRate = allShopItems.Where(si => si.si.ItemId == item.ItemId).Select(si => si.si.Price).FirstOrDefault(),
                            ItemRate = itemPriceMap[item.ItemId],
                            CreatedDate = DateTime.Now,
                            DLM = DateTime.Now
                        });
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return orderNumber;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<OrderResponse>> GetAllOrders(Guid shopId)
        {
            var data = await (
                from so in _context.ShopOrders
                where so.ShopId == shopId

                join s in _context.Shops on so.ShopId equals s.ShopId
                join st in _context.ShopTypes on s.ShopTypeId equals st.ShopTypeId into stg
                from st in stg.DefaultIfEmpty()

                join c in _context.Customers on so.CustomerId equals c.CustomerId into cg
                from c in cg.DefaultIfEmpty()

                join oi in _context.OrderItems on so.OrderId equals oi.OrderId into oig
                from oi in oig.DefaultIfEmpty()

                join i in _context.Items on oi.ItemId equals i.ItemId into ig
                from i in ig.DefaultIfEmpty()

                join sz in _context.ItemSizes on oi.SizeId equals sz.SizeId into szg
                from sz in szg.DefaultIfEmpty()

                select new
                {
                    so.OrderId,

                    // Shop
                    s.ShopId,
                    s.ShopName,
                    s.ShopAddress,
                    s.Pincode,
                    s.NearByLocation,
                    s.ShopPhone,

                    // ShopType
                    ShopTypeId = st != null ? st.ShopTypeId : Guid.Empty,
                    TypeName = st != null ? st.TypeName : null,

                    // Customer
                    Customer = c,

                    // Item
                    OrderItem = oi,
                    Item = i,
                    Size = sz
                }
            )
            .AsNoTracking()
            .ToListAsync();

            // Grouping
            var result = data
                .GroupBy(x => x.OrderId)
                .Select(g =>
                {
                    var first = g.First();

                    return new OrderResponse
                    {
                        OrderId = g.Key,

                        ShopId = first.ShopId,
                        ShopName = first.ShopName,
                        ShopTypeId = first.ShopTypeId,
                        TypeName = first.TypeName,
                        ShopAddress = first.ShopAddress,
                        Pincode = first.Pincode,
                        NearByLocation = first.NearByLocation,
                        ShopPhone = first.ShopPhone,

                        OrderBy = first.Customer == null ? null : new OrderBy
                        {
                            CustomerId = first.Customer.CustomerId,
                            CustomerName = first.Customer.CustomerName,
                            CustomerAddress = first.Customer.CustomerAddress,
                            CustomerEmail = first.Customer.CustomerEmail,
                            CustomerPhone = first.Customer.CustomerPhone,
                            Pincode = first.Customer.Pincode,
                        },

                        OrderedItems = g
                            .Where(x => x.OrderItem != null)
                            .Select(x => new OrderedItem
                            {
                                ItemId = x.OrderItem.ItemId,
                                ItemName = x.Item?.ItemName,
                                SizeId = x.OrderItem.SizeId,
                                SizeName = x.Size?.SizeName,
                                Qty = x.OrderItem.ItemQty,
                                Price = x.OrderItem.ItemRate,
                                IsActive = x.OrderItem.IsActive
                            })
                            .ToList()
                    };
                })
                .ToList();

            return result;
        }
    }
}