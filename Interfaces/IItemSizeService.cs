using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface IItemSizeService
    {
        public Task<List<ItemSizeResponse>> GetAllItemSize();
        public Task<ItemSizeResponse?> GetItemSizeById(Guid itemSizeId);
        public Task<bool> CreateItemSize(ItemSizeRequest itemSizeRequest, string loggedUser);
        public Task<bool> UpdateItemSize(Guid itemSizeId, ItemSizeRequest itemSizeRequest, string loggedUser);
        public Task<bool> DeleteItemSize(Guid itemSizeId, string loggedUser);
    }
}