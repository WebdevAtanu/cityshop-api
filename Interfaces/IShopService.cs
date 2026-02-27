using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface IShopService
    {
        public Task<List<ShopResponse>> GetAllShops();
        public Task<ShopResponse?> GetShopById(Guid shopId);
        public Task<bool> CreateShop(ShopRequest shopRequest);
        public Task<bool> UpdateShop(Guid shopId, ShopRequest shopRequest, Guid loggedUser);
        public Task<bool> DeleteShop(Guid shopId);
    }
}
