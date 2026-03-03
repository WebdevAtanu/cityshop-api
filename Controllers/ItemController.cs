using cityshop_api.DTO;
using cityshop_api.Helpers;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cityshop_api.Controllers
{
    public class ItemController : BaseController
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _itemService.GetAllItem();
            if (response == null)
            {
                return BadRequest("No item found");
            }
            return Success(response, "Items retrieved successfully");
        }

        [HttpGet]
        [Route("{itemId}")]
        public async Task<IActionResult> GetItemById(Guid itemId)
        {
            var response = await _itemService.GetItemById(itemId);
            if (response == null)
            {
                return BadRequest("Item not found");
            }
            return Success(response, "Item retrieved successfully");
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Index(ItemRequest request)
        {
            string? loggedUser = UserId;
            var result = await _itemService.CreateItem(request, loggedUser);
            if (!result) return BadRequest("Item creation failed");
            return Success(result, "Item created successfully");
        }

        [HttpPut]
        [Route("{itemId}")]
        public async Task<IActionResult> UpdateItem(Guid itemId, ItemRequest request)
        {
            string? loggedUser = UserId;
            var result = await _itemService.UpdateItem(itemId, request, loggedUser);
            if (!result) return BadRequest("Item update failed");
            return Success(result, "Item updated successfully");
        }

        [HttpPost]
        [Route("delete/{itemId}")]
        public async Task<IActionResult> DeleteItem(Guid itemId)
        {
            string? loggedUser = UserId;
            var result = await _itemService.DeleteItem(itemId, loggedUser);
            if (!result) return BadRequest("Item deletion failed");
            return Success(result, "Item deleted successfully");
        }
    }
}