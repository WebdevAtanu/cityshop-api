using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface IItemRepository
    {
        public Task<List<ItemResponse>> GetAllItem();
        public Task<ItemResponse?> GetItemById(Guid itemId);
        public Task<ItemResponse?> GetItemByName(string itemName);
        public Task<bool> CreateItem(ItemRequest itemRequest, string loggedUser);
        public Task<bool> UpdateItem(Guid itemId, ItemRequest itemRequest, string loggedUser);
        public Task<bool> DeleteItem(Guid itemId, string loggedUser);
    }
}