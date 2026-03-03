using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface IItemGroupService
    {
        public Task<List<ItemGroupResponse>> GetAllItemGroup();
        public Task<ItemGroupResponse?> GetItemGroupById(Guid itemGroupId);
        public Task<bool> CreateItemGroup(ItemGroupRequest itemGroupRequest, string loggedUser);
        public Task<bool> UpdateItemGroup(Guid itemGroupId, ItemGroupRequest itemGroupRequest, string loggedUser);
        public Task<bool> DeleteItemGroup(Guid itemGroupId, string loggedUser);
    }
}