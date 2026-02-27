using cityshop_api.DTO;
using cityshop_api.Interfaces;

namespace cityshop_api.Services
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;
        public ShopService(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<List<ShopResponse>> GetAllShops()
        {
            return await _shopRepository.GetAllShops();
        }

        public async Task<ShopResponse?> GetShopById(Guid shopId)
        {
            return await _shopRepository.GetShopById(shopId);
        }

        public async Task<bool> CreateShop(ShopRequest shopRequest)
        {
            if (!string.IsNullOrEmpty(shopRequest.GstNo))
            {
                var response = await _shopRepository.GetShopByGst(shopRequest.GstNo);
                if (response is not null)
                {
                    return false;
                }
            }
            return await _shopRepository.CreateShop(shopRequest);
        }

        public async Task<bool> UpdateShop(Guid shopId, ShopRequest shopRequest, Guid loggedUser)
        {
            var existingShop = await _shopRepository.GetShopById(shopId);
            if (existingShop is null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(shopRequest.GstNo) && existingShop.GstNo != shopRequest.GstNo)
            {
                var response = await _shopRepository.GetShopByGst(shopRequest.GstNo);
                if (response is not null)
                {
                    return false;
                }
            }
            return await _shopRepository.UpdateShop(shopId, shopRequest, loggedUser);
        }

        public async Task<bool> DeleteShop(Guid shopId)
        {
            var existingShop = await _shopRepository.GetShopById(shopId);
            if (existingShop is null)
            {
                return false;
            }
            return await _shopRepository.DeleteShop(shopId);
        }
    }
}
