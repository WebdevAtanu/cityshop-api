using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface IShopTypeRepository
    {
        public Task<List<ShopTypeResponse>> GetAllShopType();
        public Task<ShopTypeResponse?> GetShopTypeById(Guid shopTypeId);
        public Task<ShopTypeResponse?> GetShopTypeByName(string typeName);
        public Task<bool> CreateShopType(ShopTypeRequest shopTypeRequest, string loggedUser);
        public Task<bool> UpdateShopType(Guid shopTypeId, ShopTypeRequest shopTypeRequest, string loggedUser);
        public Task<bool> DeleteShopType(Guid shopTypeId, string loggedUser);
    }
}
