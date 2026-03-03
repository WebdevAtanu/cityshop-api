using cityshop_api.DTO;
using cityshop_api.Interfaces;

namespace cityshop_api.Services
{
    public class ItemCategoryService : IItemCategoryService
    {
        private readonly IItemCategoryRepository _repo;
        public ItemCategoryService(IItemCategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ItemCategoryResponse>> GetAllItemCategory()
        {
            return await _repo.GetAllItemCategory();
        }

        public async Task<ItemCategoryResponse?> GetItemCategoryById(Guid itemCategoryId)
        {
            return await _repo.GetItemCategoryById(itemCategoryId);
        }

        public async Task<bool> CreateItemCategory(ItemCategoryRequest itemCategoryRequest, string loggedUser)
        {
            if (!string.IsNullOrEmpty(itemCategoryRequest.CategoryName))
            {
                var existing = await _repo.GetItemCategoryByName(itemCategoryRequest.CategoryName);
                if (existing is not null)
                {
                    throw new Exception("Item category with the same name already exists.");
                }
            }
            return await _repo.CreateItemCategory(itemCategoryRequest, loggedUser);
        }

        public async Task<bool> UpdateItemCategory(Guid itemCategoryId, ItemCategoryRequest itemCategoryRequest, string loggedUser)
        {
            var existing = await _repo.GetItemCategoryById(itemCategoryId);
            if (existing is null) return false;
            if (!string.IsNullOrEmpty(itemCategoryRequest.CategoryName) && existing.CategoryName != itemCategoryRequest.CategoryName)
            {
                var dupe = await _repo.GetItemCategoryByName(itemCategoryRequest.CategoryName);
                if (dupe is not null) return false;
            }
            return await _repo.UpdateItemCategory(itemCategoryId, itemCategoryRequest, loggedUser);
        }

        public async Task<bool> DeleteItemCategory(Guid itemCategoryId, string loggedUser)
        {
            var existing = await _repo.GetItemCategoryById(itemCategoryId);
            if (existing is null) return false;
            return await _repo.DeleteItemCategory(itemCategoryId, loggedUser);
        }
    }
}