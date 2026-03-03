using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface IShopStatusService
    {
        public Task<List<ShopStatusResponse>> GetAllShopStatus();
        public Task<ShopStatusResponse?> GetShopStatusById(Guid statusId);
        public Task<bool> CreateShopStatus(ShopStatusRequest shopStatusRequest, string loggedUser);
        public Task<bool> UpdateShopStatus(Guid statusId, ShopStatusRequest shopStatusRequest, string loggedUser);
        public Task<bool> DeleteShopStatus(Guid statusId, string loggedUser);
    }
}