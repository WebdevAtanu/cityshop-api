using cityshop_api.DTO;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.EntityFrameworkCore;

namespace cityshop_api.Repositories
{
    public class ShopStatusRepository : IShopStatusRepository
    {
        private readonly ApplicationDBContext _context;
        public ShopStatusRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ShopStatusResponse>> GetAllShopStatus()
        {
            return await _context.ShopStatuses
                .Select(s => new ShopStatusResponse
                {
                    StatusId = s.StatusId,
                    StatusName = s.StatusName,
                    Prefix = s.Prefix,
                    Colour = s.Colour,
                    CreatedDate = s.CreatedDate,
                    CreatedBy = s.CreatedBy,
                    DLM = s.DLM,
                    ULM = s.ULM,
                    IsActive = s.IsActive
                }).ToListAsync();
        }

        public async Task<ShopStatusResponse?> GetShopStatusById(Guid statusId)
        {
            return await _context.ShopStatuses
                .Where(s => s.StatusId == statusId && s.IsActive == true)
                .Select(s => new ShopStatusResponse
                {
                    StatusId = s.StatusId,
                    StatusName = s.StatusName,
                    Prefix = s.Prefix,
                    Colour = s.Colour,
                    CreatedDate = s.CreatedDate,
                    CreatedBy = s.CreatedBy,
                    DLM = s.DLM,
                    ULM = s.ULM,
                    IsActive = s.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<ShopStatusResponse?> GetShopStatusByName(string statusName)
        {
            return await _context.ShopStatuses
                .Where(s => s.StatusName.ToLower() == statusName.ToLower() && s.IsActive == true)
                .Select(s => new ShopStatusResponse
                {
                    StatusId = s.StatusId,
                    StatusName = s.StatusName,
                    Prefix = s.Prefix,
                    Colour = s.Colour,
                    CreatedDate = s.CreatedDate,
                    CreatedBy = s.CreatedBy,
                    DLM = s.DLM,
                    ULM = s.ULM,
                    IsActive = s.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateShopStatus(ShopStatusRequest shopStatusRequest, string loggedUser)
        {
            await _context.ShopStatuses.AddAsync(new ShopStatus
            {
                StatusId = Guid.NewGuid(),
                StatusName = shopStatusRequest.StatusName,
                Prefix = shopStatusRequest.Prefix,
                Colour = shopStatusRequest.Colour,
                CreatedBy = loggedUser,
                CreatedDate = DateTime.Now,
                ULM = loggedUser,
                DLM = DateTime.Now,
                IsActive = true
            });
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateShopStatus(Guid statusId, ShopStatusRequest shopStatusRequest, string loggedUser)
        {
            var existing = await _context.ShopStatuses.FirstOrDefaultAsync(s => s.StatusId == statusId)
                          ?? throw new Exception("Shop status not found");
            existing.StatusName = shopStatusRequest.StatusName;
            existing.Prefix = shopStatusRequest.Prefix;
            existing.Colour = shopStatusRequest.Colour;
            existing.DLM = DateTime.Now;
            existing.ULM = loggedUser;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteShopStatus(Guid statusId, string loggedUser)
        {
            var existing = await _context.ShopStatuses.FirstOrDefaultAsync(s => s.StatusId == statusId)
                          ?? throw new Exception("Shop status not found");
            existing.IsActive = false;
            existing.DLM = DateTime.Now;
            existing.ULM = loggedUser;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}