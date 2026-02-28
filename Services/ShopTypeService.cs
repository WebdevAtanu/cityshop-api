using cityshop_api.DTO;
using cityshop_api.Interfaces;

namespace cityshop_api.Services
{
    public class ShopTypeService : IShopTypeService
    {
        private readonly IShopTypeRepository _shopTypeRepository;
        public ShopTypeService(IShopTypeRepository shopTypeRepository)
        {
            _shopTypeRepository = shopTypeRepository;
        }

        public async Task<List<ShopTypeResponse>> GetAllShopType()
        {
            return await _shopTypeRepository.GetAllShopType();
        }

        public async Task<ShopTypeResponse?> GetShopTypeById(Guid shopTypeId)
        {
            return await _shopTypeRepository.GetShopTypeById(shopTypeId);
        }

        public async Task<bool> CreateShopType(ShopTypeRequest shopTypeRequest, string loggedUser)
        {
            if (!string.IsNullOrEmpty(shopTypeRequest.TypeName))
            {
                var response = await _shopTypeRepository.GetShopTypeByName(shopTypeRequest.TypeName);
                if (response is not null)
                {
                    throw new Exception("Shop type with the same name already exists.");
                }
            }
            return await _shopTypeRepository.CreateShopType(shopTypeRequest, loggedUser);
        }

        public async Task<bool> UpdateShopType(Guid shopTypeId, ShopTypeRequest shopTypeRequest, string loggedUser)
        {
            var existingShop = await _shopTypeRepository.GetShopTypeById(shopTypeId);
            if (existingShop is null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(shopTypeRequest.TypeName) && existingShop.TypeName != shopTypeRequest.TypeName)
            {
                var response = await _shopTypeRepository.GetShopTypeByName(shopTypeRequest.TypeName);
                if (response is not null)
                {
                    return false;
                }
            }
            return await _shopTypeRepository.UpdateShopType(shopTypeId, shopTypeRequest, loggedUser);
        }

        public async Task<bool> DeleteShopType(Guid shopTypeId, string loggedUser)
        {
            var existingShop = await _shopTypeRepository.GetShopTypeById(shopTypeId);
            if (existingShop is null)
            {
                return false;
            }
            return await _shopTypeRepository.DeleteShopType(shopTypeId, loggedUser);
        }
    }
}
