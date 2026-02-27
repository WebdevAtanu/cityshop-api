using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface IShopRepository
    {
        public Task<List<ShopResponse>> GetAllShops();
        public Task<ShopResponse?> GetShopById(Guid shopId);
        public Task<ShopResponse?> GetShopByGst(string gstNo);
        public Task<bool> CreateShop(ShopRequest shopRequest);
        public Task<bool> UpdateShop(Guid shopId, ShopRequest shopRequest, Guid loggedUser);
        public Task<bool> DeleteShop(Guid shopId);
    }
}
