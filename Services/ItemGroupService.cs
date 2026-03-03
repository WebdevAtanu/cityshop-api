using cityshop_api.DTO;
using cityshop_api.Interfaces;

namespace cityshop_api.Services
{
    public class ItemGroupService : IItemGroupService
    {
        private readonly IItemGroupRepository _repo;
        public ItemGroupService(IItemGroupRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ItemGroupResponse>> GetAllItemGroup()
        {
            return await _repo.GetAllItemGroup();
        }

        public async Task<ItemGroupResponse?> GetItemGroupById(Guid itemGroupId)
        {
            return await _repo.GetItemGroupById(itemGroupId);
        }

        public async Task<bool> CreateItemGroup(ItemGroupRequest itemGroupRequest, string loggedUser)
        {
            if (!string.IsNullOrEmpty(itemGroupRequest.GroupName))
            {
                var existing = await _repo.GetItemGroupByName(itemGroupRequest.GroupName);
                if (existing is not null)
                {
                    throw new Exception("Item group with the same name already exists.");
                }
            }
            return await _repo.CreateItemGroup(itemGroupRequest, loggedUser);
        }

        public async Task<bool> UpdateItemGroup(Guid itemGroupId, ItemGroupRequest itemGroupRequest, string loggedUser)
        {
            var existing = await _repo.GetItemGroupById(itemGroupId);
            if (existing is null) return false;
            if (!string.IsNullOrEmpty(itemGroupRequest.GroupName) && existing.GroupName != itemGroupRequest.GroupName)
            {
                var dupe = await _repo.GetItemGroupByName(itemGroupRequest.GroupName);
                if (dupe is not null) return false;
            }
            return await _repo.UpdateItemGroup(itemGroupId, itemGroupRequest, loggedUser);
        }

        public async Task<bool> DeleteItemGroup(Guid itemGroupId, string loggedUser)
        {
            var existing = await _repo.GetItemGroupById(itemGroupId);
            if (existing is null) return false;
            return await _repo.DeleteItemGroup(itemGroupId, loggedUser);
        }
    }
}