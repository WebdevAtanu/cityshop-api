using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface IShopItemMapRepository
    {
        public Task<List<ShopItemMapResponse>> GetAllShopItems(Guid shopId);
        public Task<bool> MapShopItems(ShopItemMapRequest shopItemMapRequest, string loggedUser);
    }
}
