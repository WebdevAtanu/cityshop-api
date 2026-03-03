using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface IItemService
    {
        public Task<List<ItemResponse>> GetAllItem();
        public Task<ItemResponse?> GetItemById(Guid itemId);
        public Task<bool> CreateItem(ItemRequest itemRequest, string loggedUser);
        public Task<bool> UpdateItem(Guid itemId, ItemRequest itemRequest, string loggedUser);
        public Task<bool> DeleteItem(Guid itemId, string loggedUser);
    }
}