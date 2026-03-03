using cityshop_api.DTO;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.EntityFrameworkCore;

namespace cityshop_api.Repositories
{
    public class ItemCategoryRepository : IItemCategoryRepository
    {
        private readonly ApplicationDBContext _context;
        public ItemCategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ItemCategoryResponse>> GetAllItemCategory()
        {
            return await _context.ItemCategories.Where(x => x.IsActive == true).Select(x => new ItemCategoryResponse
            {
                ItemCategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                DLM = x.DLM,
                ULM = x.ULM,
                IsActive = x.IsActive
            }).ToListAsync();
        }

        public async Task<ItemCategoryResponse?> GetItemCategoryById(Guid itemCategoryId)
        {
            return await _context.ItemCategories.Where(x => x.CategoryId == itemCategoryId && x.IsActive == true)
                .Select(x => new ItemCategoryResponse
                {
                    ItemCategoryId = x.CategoryId,
                    CategoryName = x.CategoryName,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    DLM = x.DLM,
                    ULM = x.ULM,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<ItemCategoryResponse?> GetItemCategoryByName(string categoryName)
        {
            return await _context.ItemCategories.Where(x => x.CategoryName.ToLower() == categoryName.ToLower() && x.IsActive == true)
                .Select(x => new ItemCategoryResponse
                {
                    ItemCategoryId = x.CategoryId,
                    CategoryName = x.CategoryName,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    DLM = x.DLM,
                    ULM = x.ULM,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateItemCategory(ItemCategoryRequest itemCategoryRequest, string loggedUser)
        {
            await _context.ItemCategories.AddAsync(new ItemCategory
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = itemCategoryRequest.CategoryName,
                CreatedBy = loggedUser,
                CreatedDate = DateTime.Now,
                ULM = loggedUser,
                DLM = DateTime.Now,
                IsActive = true
            });
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateItemCategory(Guid itemCategoryId, ItemCategoryRequest itemCategoryRequest, string loggedUser)
        {
            var existing = await _context.ItemCategories.FirstOrDefaultAsync(x => x.CategoryId == itemCategoryId)
                          ?? throw new Exception("Item category not found");
            existing.CategoryName = itemCategoryRequest.CategoryName;
            existing.DLM = DateTime.Now;
            existing.ULM = loggedUser;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteItemCategory(Guid itemCategoryId, string loggedUser)
        {
            var existing = await _context.ItemCategories.FirstOrDefaultAsync(x => x.CategoryId == itemCategoryId)
                          ?? throw new Exception("Item category not found");
            existing.IsActive = false;
            existing.DLM = DateTime.Now;
            existing.ULM = loggedUser;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}