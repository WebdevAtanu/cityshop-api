using cityshop_api.DTO;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace cityshop_api.Repositories
{
    public class ShopItemMapRepository : IShopItemMapRepository
    {
        private readonly ApplicationDBContext _context;
        public ShopItemMapRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ShopItemMapResponse>> GetAllShopItems(Guid shopId)
        {
            var response = await (from s in _context.Shops
                                  join si in _context.ShopItems on s.ShopId equals si.ShopId into shopItemIoin
                                  from si in shopItemIoin.DefaultIfEmpty()

                                  join i in _context.Items on si.ItemId equals i.ItemId into itemDetailJoin
                                  from i in itemDetailJoin.DefaultIfEmpty()

                                  join iz in _context.ItemSizes on si.SizeId equals iz.SizeId into itemSizeJoin
                                  from iz in itemSizeJoin.DefaultIfEmpty()

                                  where s.ShopId == shopId
                                  select new
                                  {
                                      s.ShopId,
                                      s.ShopName,
                                      i.ItemId,
                                      i.ItemName,
                                      iz.SizeId,
                                      iz.SizeName,
                                      si.Qty,
                                      si.Price,
                                      si.Discount,
                                      si.StockQty,
                                      si.IsActive
                                  }).ToListAsync();

            var data = response.GroupBy(r => new { r.ShopId, r.ShopName })
                .Select(g => new ShopItemMapResponse
                {
                    ShopId = g.Key.ShopId,
                    ShopName = g.Key.ShopName,
                    MapItems = g.Select(g => new MapItem
                    {
                        ItemId = g.ItemId,
                        ItemName = g.ItemName,
                        SizeId = g.SizeId,
                        SizeName = g.SizeName,
                        Qty = g.Qty,
                        Price = g.Price,
                        Discount = g.Discount,
                        StockQty = g.StockQty,
                        IsActive = g.IsActive,

                    }).ToList(),
                }).ToList();

            return data;

        }

        public async Task<bool> MapShopItems(ShopItemMapRequest shopItemMapRequest, string loggedUser)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var previousMappings = await _context.ShopItems.Where(si => si.ShopId == shopItemMapRequest.ShopId).ToListAsync();

                if (previousMappings.Any())
                {
                    _context.ShopItems.RemoveRange(previousMappings);
                }

                DateTime now = DateTime.Now;

                if (shopItemMapRequest.ShopItems is not null)
                {
                    foreach (var newMapping in shopItemMapRequest.ShopItems)
                    {
                        _context.ShopItems.Add(new ShopItem
                        {
                            ShopId = shopItemMapRequest.ShopId,
                            ItemId = newMapping.ItemId,
                            SizeId = newMapping.SizeId,
                            Qty = newMapping.Qty,
                            Price = newMapping.Price,
                            Discount = newMapping.Discount,
                            StockQty = newMapping.StockQty,
                            IsActive = newMapping.IsActive,
                            CreatedBy = loggedUser,
                            CreatedDate = now,
                            ULM = loggedUser,
                            DLM = now,
                        });
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
