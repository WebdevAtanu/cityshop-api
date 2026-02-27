using cityshop_api.DTO;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.EntityFrameworkCore;

namespace cityshop_api.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly ApplicationDBContext _context;
        public ShopRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ShopResponse>> GetAllShops()
        {
            var response = await (from s in _context.Shops
                                  join st in _context.ShopTypes on s.ShopTypeId equals st.ShopTypeId
                                  into shopTypegroup
                                  from st in shopTypegroup.DefaultIfEmpty()
                                  join ss in _context.ShopStatuses on s.StatusId equals ss.StatusId
                                  into shopStatusGroup
                                  from ss in shopStatusGroup.DefaultIfEmpty()
                                  select new ShopResponse
                                  {
                                      ShopId = s.ShopId,
                                      ShopName = s.ShopName,
                                      ShopAddress = s.ShopAddress,
                                      Pincode = s.Pincode,
                                      ShopPhone = s.ShopPhone,
                                      ShopLogo = s.ShopLogo,
                                      ShopImage = s.ShopImage,
                                      GstNo = s.GstNo,
                                      Latitude = s.Latitude,
                                      Longitude = s.Longitude,
                                      StatusId = s.StatusId,
                                      StatusName = ss.StatusName,
                                      Prefix = ss.Prefix,
                                      Colour = ss.Colour,
                                      ShopTypeId = s.ShopTypeId,
                                      TypeName = st.TypeName,
                                      OpeningTime = s.OpeningTime,
                                      ClosingTime = s.ClosingTime,
                                      NearByLocation = s.NearByLocation,
                                      CreatedDate = s.CreatedDate,
                                      CreatedBy = s.CreatedBy,
                                      DLM = s.DLM,
                                      ULM = s.ULM,
                                      IsActive = s.IsActive
                                  }).ToListAsync();
            return response;

        }

        public async Task<ShopResponse?> GetShopById(Guid shopId)
        {
            NotImplementedException ex = new NotImplementedException();
            throw ex;
        }

        public async Task<ShopResponse?> GetShopByGst(string gstNo)
        {
            return await _context.Shops.FirstOrDefaultAsync(s => s.GstNo == gstNo) is Shop shop ? new ShopResponse
            {
                ShopId = shop.ShopId,
                ShopName = shop.ShopName,
                ShopAddress = shop.ShopAddress,
                Pincode = shop.Pincode,
                ShopPhone = shop.ShopPhone,
                ShopLogo = shop.ShopLogo,
                ShopImage = shop.ShopImage,
                GstNo = shop.GstNo,
                Latitude = shop.Latitude,
                Longitude = shop.Longitude,
                StatusId = shop.StatusId,
                ShopTypeId = shop.ShopTypeId,
                OpeningTime = shop.OpeningTime,
                ClosingTime = shop.ClosingTime,
                NearByLocation = shop.NearByLocation,
                CreatedDate = shop.CreatedDate,
                CreatedBy = shop.CreatedBy,
                DLM = shop.DLM,
                ULM = shop.ULM,
                IsActive = shop.IsActive
            } : null;
        }

        public async Task<bool> CreateShop(ShopRequest shopRequest)
        {
            await _context.Shops.AddAsync(new Shop
            {
                ShopId = Guid.NewGuid(),
                ShopName = shopRequest.ShopName,
                ShopAddress = shopRequest.ShopAddress,
                Pincode = shopRequest.Pincode,
                ShopPhone = shopRequest.ShopPhone,
                ShopLogo = shopRequest.ShopLogo,
                ShopImage = shopRequest.ShopImage,
                GstNo = shopRequest.GstNo,
                Latitude = shopRequest.Latitude,
                Longitude = shopRequest.Longitude,
                StatusId = shopRequest.StatusId,
                ShopTypeId = shopRequest.ShopTypeId,
                OpeningTime = DateTime.TryParse(shopRequest.OpeningTime, out DateTime openingTime) ? openingTime : null,
                ClosingTime = DateTime.TryParse(shopRequest.ClosingTime, out DateTime closingTime) ? closingTime : null,
                NearByLocation = shopRequest.NearByLocation,
                IsActive = shopRequest.IsActive ?? true
            });
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public Task<bool> UpdateShop(Guid shopId, ShopRequest shopRequest, Guid loggedUser)
        {
            NotImplementedException ex = new NotImplementedException();
            throw ex;
        }

        public Task<bool> DeleteShop(Guid shopId)
        {
            NotImplementedException ex = new NotImplementedException();
            throw ex;
        }
    }
}
