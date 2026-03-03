using cityshop_api.DTO;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cityshop_api.Controllers
{
    public class ItemGroupController : BaseController
    {
        private readonly IItemGroupService _itemGroupService;
        public ItemGroupController(IItemGroupService itemGroupService)
        {
            _itemGroupService = itemGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _itemGroupService.GetAllItemGroup();
            if (response == null)
            {
                return BadRequest("No item groups found");
            }
            return Success(response, "Item groups retrieved successfully");
        }

        [HttpGet]
        [Route("{itemGroupId}")]
        public async Task<IActionResult> GetItemGroupById(Guid itemGroupId)
        {
            var response = await _itemGroupService.GetItemGroupById(itemGroupId);
            if (response == null)
            {
                return BadRequest("Item group not found");
            }
            return Success(response, "Item group retrieved successfully");
        }

        [HttpPost]
        public async Task<IActionResult> Index(ItemGroupRequest request)
        {
            string? loggedUser = UserId;
            var result = await _itemGroupService.CreateItemGroup(request, loggedUser);
            if (!result) return BadRequest("Item group creation failed");
            return Success(result, "Item group created successfully");
        }

        [HttpPut]
        [Route("{itemGroupId}")]
        public async Task<IActionResult> UpdateItemGroup(Guid itemGroupId, ItemGroupRequest request)
        {
            string? loggedUser = UserId;
            var result = await _itemGroupService.UpdateItemGroup(itemGroupId, request, loggedUser);
            if (!result) return BadRequest("Item group update failed");
            return Success(result, "Item group updated successfully");
        }

        [HttpPost]
        [Route("delete/{itemGroupId}")]
        public async Task<IActionResult> DeleteItemGroup(Guid itemGroupId)
        {
            string? loggedUser = UserId;
            var result = await _itemGroupService.DeleteItemGroup(itemGroupId, loggedUser);
            if (!result) return BadRequest("Item group deletion failed");
            return Success(result, "Item group deleted successfully");
        }
    }
}