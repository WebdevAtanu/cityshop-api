using cityshop_api.DTO;
using cityshop_api.Interfaces;

namespace cityshop_api.Services
{
    public class ShopStatusService : IShopStatusService
    {
        private readonly IShopStatusRepository _shopStatusRepository;
        public ShopStatusService(IShopStatusRepository shopStatusRepository)
        {
            _shopStatusRepository = shopStatusRepository;
        }

        public async Task<List<ShopStatusResponse>> GetAllShopStatus()
        {
            return await _shopStatusRepository.GetAllShopStatus();
        }

        public async Task<ShopStatusResponse?> GetShopStatusById(Guid statusId)
        {
            return await _shopStatusRepository.GetShopStatusById(statusId);
        }

        public async Task<bool> CreateShopStatus(ShopStatusRequest shopStatusRequest, string loggedUser)
        {
            if (!string.IsNullOrEmpty(shopStatusRequest.StatusName))
            {
                var response = await _shopStatusRepository.GetShopStatusByName(shopStatusRequest.StatusName);
                if (response is not null)
                {
                    throw new Exception("Shop status with the same name already exists.");
                }
            }
            return await _shopStatusRepository.CreateShopStatus(shopStatusRequest, loggedUser);
        }

        public async Task<bool> UpdateShopStatus(Guid statusId, ShopStatusRequest shopStatusRequest, string loggedUser)
        {
            var existing = await _shopStatusRepository.GetShopStatusById(statusId);
            if (existing is null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(shopStatusRequest.StatusName) && existing.StatusName != shopStatusRequest.StatusName)
            {
                var response = await _shopStatusRepository.GetShopStatusByName(shopStatusRequest.StatusName);
                if (response is not null)
                {
                    return false;
                }
            }
            return await _shopStatusRepository.UpdateShopStatus(statusId, shopStatusRequest, loggedUser);
        }

        public async Task<bool> DeleteShopStatus(Guid statusId, string loggedUser)
        {
            var existing = await _shopStatusRepository.GetShopStatusById(statusId);
            if (existing is null)
            {
                return false;
            }
            return await _shopStatusRepository.DeleteShopStatus(statusId, loggedUser);
        }
    }
}