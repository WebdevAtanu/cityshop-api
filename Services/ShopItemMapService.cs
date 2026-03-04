using cityshop_api.DTO;
using cityshop_api.Interfaces;

namespace cityshop_api.Services
{
    public class ShopItemMapService : IShopItemMapService
    {
        private readonly IShopItemMapRepository _shopItemMapRepository;
        public ShopItemMapService(IShopItemMapRepository shopItemMapRepository)
        {
            _shopItemMapRepository = shopItemMapRepository;
        }

        public async Task<List<ShopItemMapResponse>> GetAllShopItems(Guid shopId)
        {
            return await _shopItemMapRepository.GetAllShopItems(shopId);
        }

        public async Task<bool> MapShopItems(ShopItemMapRequest shopItemMapRequest, string loggedUser)
        {
            return await _shopItemMapRepository.MapShopItems(shopItemMapRequest, loggedUser);
        }
    }
}
