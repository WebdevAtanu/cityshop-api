using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface IItemCategoryRepository
    {
        public Task<List<ItemCategoryResponse>> GetAllItemCategory();
        public Task<ItemCategoryResponse?> GetItemCategoryById(Guid itemCategoryId);
        public Task<ItemCategoryResponse?> GetItemCategoryByName(string categoryName);
        public Task<bool> CreateItemCategory(ItemCategoryRequest itemCategoryRequest, string loggedUser);
        public Task<bool> UpdateItemCategory(Guid itemCategoryId, ItemCategoryRequest itemCategoryRequest, string loggedUser);
        public Task<bool> DeleteItemCategory(Guid itemCategoryId, string loggedUser);
    }
}