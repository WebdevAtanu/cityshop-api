using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface IShopStatusRepository
    {
        public Task<List<ShopStatusResponse>> GetAllShopStatus();
        public Task<ShopStatusResponse?> GetShopStatusById(Guid statusId);
        public Task<ShopStatusResponse?> GetShopStatusByName(string statusName);
        public Task<bool> CreateShopStatus(ShopStatusRequest shopStatusRequest, string loggedUser);
        public Task<bool> UpdateShopStatus(Guid statusId, ShopStatusRequest shopStatusRequest, string loggedUser);
        public Task<bool> DeleteShopStatus(Guid statusId, string loggedUser);
    }
}