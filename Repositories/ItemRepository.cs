using cityshop_api.DTO;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace cityshop_api.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDBContext _context;
        public ItemRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ItemResponse>> GetAllItem()
        {
            var response = await (from i in _context.Items
                                  join ig in _context.ItemGroups
                                  on i.ItemGroupId equals ig.GroupId into itemGroupJoin
                                  from ig in itemGroupJoin.DefaultIfEmpty()
                                  join ic in _context.ItemCategories
                                  on i.ItemCategoryId equals ic.CategoryId into itemCategoryJoin
                                  from ic in itemCategoryJoin.DefaultIfEmpty()
                                  join iz in _context.ItemSizes
                                  on i.ItemSizeId equals iz.SizeId into itemSizeJoin
                                  from iz in itemSizeJoin.DefaultIfEmpty()
                                  select new ItemResponse
                                  {
                                      ItemId = i.ItemId,
                                      ItemName = i.ItemName,
                                      ItemDescription = i.ItemDescription,
                                      ItemGroupId = i.ItemGroupId,
                                      ItemgroupName = ig.GroupName,
                                      ItemCategoryId = i.ItemCategoryId,
                                      ItemCategoryName = ic.CategoryName,
                                      ItemSizeId = i.ItemSizeId,
                                      ItemSizeName = iz.SizeName,
                                      ItemCode = i.ItemCode,
                                      ItemImage = i.ItemImage,
                                      CreatedDate = i.CreatedDate,
                                      CreatedBy = i.CreatedBy,
                                      DLM = i.DLM,
                                      ULM = i.ULM,
                                      IsActive = i.IsActive
                                  }).ToListAsync();
            return response;
        }

        public async Task<ItemResponse?> GetItemById(Guid itemId)
        {
            return await _context.Items.Where(x => x.ItemId == itemId && x.IsActive == true)
                .Select(x => new ItemResponse
                {
                    ItemId = x.ItemId,
                    ItemName = x.ItemName,
                    ItemDescription = x.ItemDescription,
                    ItemGroupId = x.ItemGroupId,
                    ItemCategoryId = x.ItemCategoryId,
                    ItemSizeId = x.ItemSizeId,
                    ItemCode = x.ItemCode,
                    ItemImage = x.ItemImage,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    DLM = x.DLM,
                    ULM = x.ULM,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<ItemResponse?> GetItemByName(string itemName)
        {
            return await _context.Items.Where(x => x.ItemName.ToLower() == itemName.ToLower() && x.IsActive == true)
                .Select(x => new ItemResponse
                {
                    ItemId = x.ItemId,
                    ItemName = x.ItemName,
                    ItemDescription = x.ItemDescription,
                    ItemGroupId = x.ItemGroupId,
                    ItemCategoryId = x.ItemCategoryId,
                    ItemSizeId = x.ItemSizeId,
                    ItemCode = x.ItemCode,
                    ItemImage = x.ItemImage,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    DLM = x.DLM,
                    ULM = x.ULM,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateItem(ItemRequest itemRequest, string loggedUser)
        {
            await _context.Items.AddAsync(new Item
            {
                ItemId = Guid.NewGuid(),
                ItemName = itemRequest.ItemName,
                ItemDescription = itemRequest.ItemDescription,
                ItemGroupId = itemRequest.ItemGroupId,
                ItemCategoryId = itemRequest.ItemCategoryId,
                ItemCode = itemRequest.ItemCode,
                ItemImage = itemRequest.ItemImage,
                ItemSizeId = itemRequest.ItemSizeId,
                CreatedBy = loggedUser,
                CreatedDate = DateTime.Now,
                ULM = loggedUser,
                DLM = DateTime.Now,
                IsActive = true
            });
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateItem(Guid itemId, ItemRequest itemRequest, string loggedUser)
        {
            var existing = await _context.Items.FirstOrDefaultAsync(x => x.ItemId == itemId)
                          ?? throw new Exception("Item not found");
            existing.ItemName = itemRequest.ItemName;
            existing.ItemDescription = itemRequest.ItemDescription;
            existing.ItemGroupId = itemRequest.ItemGroupId;
            existing.ItemCategoryId = itemRequest.ItemCategoryId;
            existing.ItemCode = itemRequest.ItemCode;
            existing.ItemImage = itemRequest.ItemImage;
            existing.ItemSizeId = itemRequest.ItemSizeId;
            existing.DLM = DateTime.Now;
            existing.ULM = loggedUser;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteItem(Guid itemId, string loggedUser)
        {
            var existing = await _context.Items.FirstOrDefaultAsync(x => x.ItemId == itemId)
                          ?? throw new Exception("Item not found");
            existing.IsActive = false;
            existing.DLM = DateTime.Now;
            existing.ULM = loggedUser;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}