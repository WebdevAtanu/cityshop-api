using cityshop_api.DTO;
using cityshop_api.Interfaces;

namespace cityshop_api.Services
{
    public class ItemSizeService : IItemSizeService
    {
        private readonly IItemSizeRepository _repo;
        public ItemSizeService(IItemSizeRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ItemSizeResponse>> GetAllItemSize()
        {
            return await _repo.GetAllItemSize();
        }

        public async Task<ItemSizeResponse?> GetItemSizeById(Guid itemSizeId)
        {
            return await _repo.GetItemSizeById(itemSizeId);
        }

        public async Task<bool> CreateItemSize(ItemSizeRequest itemSizeRequest, string loggedUser)
        {
            if (!string.IsNullOrEmpty(itemSizeRequest.SizeName))
            {
                var existing = await _repo.GetItemSizeByName(itemSizeRequest.SizeName);
                if (existing is not null)
                {
                    throw new Exception("Item size with the same name already exists.");
                }
            }
            return await _repo.CreateItemSize(itemSizeRequest, loggedUser);
        }

        public async Task<bool> UpdateItemSize(Guid itemSizeId, ItemSizeRequest itemSizeRequest, string loggedUser)
        {
            var existing = await _repo.GetItemSizeById(itemSizeId);
            if (existing is null) return false;
            if (!string.IsNullOrEmpty(itemSizeRequest.SizeName) && existing.SizeName != itemSizeRequest.SizeName)
            {
                var dupe = await _repo.GetItemSizeByName(itemSizeRequest.SizeName);
                if (dupe is not null) return false;
            }
            return await _repo.UpdateItemSize(itemSizeId, itemSizeRequest, loggedUser);
        }

        public async Task<bool> DeleteItemSize(Guid itemSizeId, string loggedUser)
        {
            var existing = await _repo.GetItemSizeById(itemSizeId);
            if (existing is null) return false;
            return await _repo.DeleteItemSize(itemSizeId, loggedUser);
        }
    }
}