using cityshop_api.DTO;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.EntityFrameworkCore;

namespace cityshop_api.Repositories
{
    public class ShopTypeRepository : IShopTypeRepository
    {
        private readonly ApplicationDBContext _context;
        public ShopTypeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ShopTypeResponse>> GetAllShopType()
        {
            var response = await _context.ShopTypes.Where(st => st.IsActive == true).Select(st => new ShopTypeResponse
            {
                ShopTypeId = st.ShopTypeId,
                TypeName = st.TypeName,
                CreatedDate = st.CreatedDate,
                CreatedBy = st.CreatedBy,
                DLM = st.DLM,
                ULM = st.ULM,
                IsActive = st.IsActive
            }).ToListAsync();
            return response;

        }

        public async Task<ShopTypeResponse?> GetShopTypeById(Guid shopTypeId)
        {
            var response = await _context.ShopTypes.Where(st => st.ShopTypeId == shopTypeId && st.IsActive == true).Select(st => new ShopTypeResponse
            {
                ShopTypeId = st.ShopTypeId,
                TypeName = st.TypeName,
                CreatedDate = st.CreatedDate,
                CreatedBy = st.CreatedBy,
                DLM = st.DLM,
                ULM = st.ULM,
                IsActive = st.IsActive
            }).FirstOrDefaultAsync();
            return response;
        }

        public async Task<ShopTypeResponse?> GetShopTypeByName(string typeName)
        {
            return await _context.ShopTypes.Where(st => st.TypeName.ToLower() == typeName.ToLower() && st.IsActive == true)
                 .Select(st => new ShopTypeResponse
                 {
                     ShopTypeId = st.ShopTypeId,
                     TypeName = st.TypeName,
                     CreatedDate = st.CreatedDate,
                     CreatedBy = st.CreatedBy,
                     DLM = st.DLM,
                     ULM = st.ULM,
                     IsActive = st.IsActive
                 }).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateShopType(ShopTypeRequest shopTypeRequest, string loggedUser)
        {
            await _context.ShopTypes.AddAsync(new ShopType
            {
                ShopTypeId = Guid.NewGuid(),
                TypeName = shopTypeRequest.TypeName,
                CreatedBy = loggedUser,
                CreatedDate = DateTime.Now,
                IsActive = true
            });
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateShopType(Guid shopTypeId, ShopTypeRequest shopTypeRequest, string loggedUser)
        {
            var existingShopType = await _context.ShopTypes.FirstOrDefaultAsync(s => s.ShopTypeId == shopTypeId) ?? throw new Exception("Shop type not found");
            existingShopType.TypeName = shopTypeRequest.TypeName;
            existingShopType.DLM = DateTime.Now;
            existingShopType.ULM = loggedUser;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteShopType(Guid shopTypeId, string loggedUser)
        {
            var existingShoptype = await _context.ShopTypes.FirstOrDefaultAsync(s => s.ShopTypeId == shopTypeId) ?? throw new Exception("Shop not found");

            existingShoptype.IsActive = false;
            existingShoptype.DLM = DateTime.Now;
            existingShoptype.ULM = loggedUser;

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
