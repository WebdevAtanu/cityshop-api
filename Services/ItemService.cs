using cityshop_api.DTO;
using cityshop_api.Interfaces;

namespace cityshop_api.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repo;
        public ItemService(IItemRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ItemResponse>> GetAllItem()
        {
            return await _repo.GetAllItem();
        }

        public async Task<ItemResponse?> GetItemById(Guid itemId)
        {
            return await _repo.GetItemById(itemId);
        }

        public async Task<bool> CreateItem(ItemRequest itemRequest, string loggedUser)
        {
            if (!string.IsNullOrEmpty(itemRequest.ItemName))
            {
                var existing = await _repo.GetItemByName(itemRequest.ItemName);
                if (existing is not null)
                {
                    throw new Exception("Item category with the same name already exists.");
                }
            }
            return await _repo.CreateItem(itemRequest, loggedUser);
        }

        public async Task<bool> UpdateItem(Guid itemId, ItemRequest itemRequest, string loggedUser)
        {
            var existing = await _repo.GetItemById(itemId);
            if (existing is null) return false;
            if (!string.IsNullOrEmpty(itemRequest.ItemName) && existing.ItemName != itemRequest.ItemName)
            {
                var dupe = await _repo.GetItemByName(itemRequest.ItemName);
                if (dupe is not null) return false;
            }
            return await _repo.UpdateItem(itemId, itemRequest, loggedUser);
        }

        public async Task<bool> DeleteItem(Guid itemId, string loggedUser)
        {
            var existing = await _repo.GetItemById(itemId);
            if (existing is null) return false;
            return await _repo.DeleteItem(itemId, loggedUser);
        }
    }
}