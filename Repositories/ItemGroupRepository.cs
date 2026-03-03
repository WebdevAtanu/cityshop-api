using cityshop_api.DTO;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.EntityFrameworkCore;

namespace cityshop_api.Repositories
{
    public class ItemGroupRepository : IItemGroupRepository
    {
        private readonly ApplicationDBContext _context;
        public ItemGroupRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ItemGroupResponse>> GetAllItemGroup()
        {
            return await _context.ItemGroups.Where(x => x.IsActive == true).Select(x => new ItemGroupResponse
            {
                ItemGroupId = x.GroupId,
                GroupName = x.GroupName,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                DLM = x.DLM,
                ULM = x.ULM,
                IsActive = x.IsActive
            }).ToListAsync();
        }

        public async Task<ItemGroupResponse?> GetItemGroupById(Guid itemGroupId)
        {
            return await _context.ItemGroups.Where(x => x.GroupId == itemGroupId && x.IsActive == true)
                .Select(x => new ItemGroupResponse
                {
                    ItemGroupId = x.GroupId,
                    GroupName = x.GroupName,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    DLM = x.DLM,
                    ULM = x.ULM,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<ItemGroupResponse?> GetItemGroupByName(string groupName)
        {
            return await _context.ItemGroups.Where(x => x.GroupName.ToLower() == groupName.ToLower() && x.IsActive == true)
                .Select(x => new ItemGroupResponse
                {
                    ItemGroupId = x.GroupId,
                    GroupName = x.GroupName,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    DLM = x.DLM,
                    ULM = x.ULM,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateItemGroup(ItemGroupRequest itemGroupRequest, string loggedUser)
        {
            await _context.ItemGroups.AddAsync(new ItemGroup
            {
                GroupId = Guid.NewGuid(),
                GroupName = itemGroupRequest.GroupName,
                CreatedBy = loggedUser,
                CreatedDate = DateTime.Now,
                ULM = loggedUser,
                DLM = DateTime.Now,
                IsActive = true
            });
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateItemGroup(Guid itemGroupId, ItemGroupRequest itemGroupRequest, string loggedUser)
        {
            var existing = await _context.ItemGroups.FirstOrDefaultAsync(x => x.GroupId == itemGroupId)
                          ?? throw new Exception("Item group not found");
            existing.GroupName = itemGroupRequest.GroupName;
            existing.DLM = DateTime.Now;
            existing.ULM = loggedUser;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteItemGroup(Guid itemGroupId, string loggedUser)
        {
            var existing = await _context.ItemGroups.FirstOrDefaultAsync(x => x.GroupId == itemGroupId)
                          ?? throw new Exception("Item group not found");
            existing.IsActive = false;
            existing.DLM = DateTime.Now;
            existing.ULM = loggedUser;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}