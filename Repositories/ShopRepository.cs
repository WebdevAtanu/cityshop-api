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
                                      ShopCode = s.ShopCode,
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
            var response = await (from s in _context.Shops
                                  join st in _context.ShopTypes on s.ShopTypeId equals st.ShopTypeId
                                  into shopTypeGroup
                                  from st in shopTypeGroup.DefaultIfEmpty()
                                  join ss in _context.ShopStatuses on s.StatusId equals ss.StatusId
                                  into shopStatusGroup
                                  from ss in shopStatusGroup.DefaultIfEmpty()
                                  where s.ShopId == shopId && s.IsActive == true
                                  select new ShopResponse
                                  {
                                      ShopId = s.ShopId,
                                      ShopName = s.ShopName,
                                      ShopAddress = s.ShopAddress,
                                      Pincode = s.Pincode,
                                      ShopCode = s.ShopCode,
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
                                  }).FirstOrDefaultAsync();
            return response;
        }

        public async Task<ShopResponse?> GetShopByGst(string gstNo)
        {
            return await _context.Shops.FirstOrDefaultAsync(s => s.GstNo == gstNo) is Shop shop ? new ShopResponse
            {
                ShopId = shop.ShopId,
                ShopName = shop.ShopName,
                ShopAddress = shop.ShopAddress,
                Pincode = shop.Pincode,
                ShopCode = shop.ShopCode,
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

        private async Task<string> GenerateShopCode(string shopName)
        {
            // Create prefix from shop name
            var words = shopName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string prefix = string.Concat(words.Select(w => char.ToUpper(w[0])));

            // Get last shop code with same prefix
            string? lastCode = await _context.Shops
                .Where(s => s.ShopCode.StartsWith(prefix))
                .OrderByDescending(s => s.ShopCode)
                .Select(s => s.ShopCode)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (!string.IsNullOrEmpty(lastCode))
            {
                string numberPart = lastCode.Substring(prefix.Length);
                nextNumber = int.Parse(numberPart) + 1;
            }

            return $"{prefix}{nextNumber:D2}";
        }

        public async Task<bool> CreateShop(ShopRequest shopRequest, string loggedUser)
        {
            string shopCode = await GenerateShopCode(shopRequest.ShopName ?? "SC");
            await _context.Shops.AddAsync(new Shop
            {
                ShopId = Guid.NewGuid(),
                ShopName = shopRequest.ShopName,
                ShopAddress = shopRequest.ShopAddress,
                Pincode = shopRequest.Pincode,
                ShopPhone = shopRequest.ShopPhone,
                ShopCode = shopCode,
                ShopLogo = shopRequest.ShopLogo,
                ShopImage = shopRequest.ShopImage,
                GstNo = shopRequest.GstNo,
                Latitude = shopRequest.Latitude,
                Longitude = shopRequest.Longitude,
                StatusId = shopRequest.StatusId,
                ShopTypeId = shopRequest.ShopTypeId,
                OpeningTime = shopRequest.OpeningTime,
                ClosingTime = shopRequest.ClosingTime,
                NearByLocation = shopRequest.NearByLocation,
                IsActive = shopRequest.IsActive ?? true,
                CreatedBy = loggedUser.ToString(),
                ULM = loggedUser,
                DLM = DateTime.Now
            });
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateShop(Guid shopId, ShopRequest shopRequest, string loggedUser)
        {
            var existingShop = await _context.Shops.FirstOrDefaultAsync(s => s.ShopId == shopId) ?? throw new Exception("Shop not found");

            existingShop.ShopName = shopRequest.ShopName;
            existingShop.ShopAddress = shopRequest.ShopAddress;
            existingShop.Pincode = shopRequest.Pincode;
            existingShop.ShopPhone = shopRequest.ShopPhone;
            existingShop.ShopLogo = shopRequest.ShopLogo;
            existingShop.ShopImage = shopRequest.ShopImage;
            existingShop.GstNo = shopRequest.GstNo;
            existingShop.Latitude = shopRequest.Latitude;
            existingShop.Longitude = shopRequest.Longitude;
            existingShop.StatusId = shopRequest.StatusId;
            existingShop.ShopTypeId = shopRequest.ShopTypeId;
            existingShop.OpeningTime = shopRequest.OpeningTime ?? existingShop.OpeningTime;
            existingShop.ClosingTime = shopRequest.ClosingTime ?? existingShop.ClosingTime;
            existingShop.NearByLocation = shopRequest.NearByLocation;
            existingShop.IsActive = shopRequest.IsActive ?? existingShop.IsActive;
            existingShop.ULM = loggedUser;
            existingShop.DLM = DateTime.Now;

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteShop(Guid shopId, string loggedUser)
        {
            var existingShop = await _context.Shops.FirstOrDefaultAsync(s => s.ShopId == shopId) ?? throw new Exception("Shop not found");

            existingShop.IsActive = false;
            existingShop.DLM = DateTime.Now;
            existingShop.ULM = loggedUser;

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
