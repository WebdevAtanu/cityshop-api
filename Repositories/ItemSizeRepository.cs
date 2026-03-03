using cityshop_api.DTO;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.EntityFrameworkCore;

namespace cityshop_api.Repositories
{
    public class ItemSizeRepository : IItemSizeRepository
    {
        private readonly ApplicationDBContext _context;
        public ItemSizeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ItemSizeResponse>> GetAllItemSize()
        {
            return await _context.ItemSizes.Where(x => x.IsActive == true).Select(x => new ItemSizeResponse
            {
                ItemSizeId = x.SizeId,
                SizeName = x.SizeName,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                DLM = x.DLM,
                ULM = x.ULM,
                IsActive = x.IsActive
            }).ToListAsync();
        }

        public async Task<ItemSizeResponse?> GetItemSizeById(Guid itemSizeId)
        {
            return await _context.ItemSizes.Where(x => x.SizeId == itemSizeId && x.IsActive == true)
                .Select(x => new ItemSizeResponse
                {
                    ItemSizeId = x.SizeId,
                    SizeName = x.SizeName,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    DLM = x.DLM,
                    ULM = x.ULM,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<ItemSizeResponse?> GetItemSizeByName(string sizeName)
        {
            return await _context.ItemSizes.Where(x => x.SizeName.ToLower() == sizeName.ToLower() && x.IsActive == true)
                .Select(x => new ItemSizeResponse
                {
                    ItemSizeId = x.SizeId,
                    SizeName = x.SizeName,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    DLM = x.DLM,
                    ULM = x.ULM,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateItemSize(ItemSizeRequest itemSizeRequest, string loggedUser)
        {
            await _context.ItemSizes.AddAsync(new ItemSize
            {
                SizeId = Guid.NewGuid(),
                SizeName = itemSizeRequest.SizeName,
                CreatedBy = loggedUser,
                CreatedDate = DateTime.Now,
                ULM = loggedUser,
                DLM = DateTime.Now,
                IsActive = true
            });
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateItemSize(Guid itemSizeId, ItemSizeRequest itemSizeRequest, string loggedUser)
        {
            var existing = await _context.ItemSizes.FirstOrDefaultAsync(x => x.SizeId == itemSizeId)
                          ?? throw new Exception("Item size not found");
            existing.SizeName = itemSizeRequest.SizeName;
            existing.DLM = DateTime.Now;
            existing.ULM = loggedUser;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteItemSize(Guid itemSizeId, string loggedUser)
        {
            var existing = await _context.ItemSizes.FirstOrDefaultAsync(x => x.SizeId == itemSizeId)
                          ?? throw new Exception("Item size not found");
            existing.IsActive = false;
            existing.DLM = DateTime.Now;
            existing.ULM = loggedUser;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}