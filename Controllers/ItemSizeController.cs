using cityshop_api.DTO;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cityshop_api.Controllers
{
    public class ItemSizeController : BaseController
    {
        private readonly IItemSizeService _itemSizeService;
        public ItemSizeController(IItemSizeService itemSizeService)
        {
            _itemSizeService = itemSizeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _itemSizeService.GetAllItemSize();
            if (response == null)
            {
                return BadRequest("No item sizes found");
            }
            return Success(response, "Item sizes retrieved successfully");
        }

        [HttpGet]
        [Route("{itemSizeId}")]
        public async Task<IActionResult> GetItemSizeById(Guid itemSizeId)
        {
            var response = await _itemSizeService.GetItemSizeById(itemSizeId);
            if (response == null)
            {
                return BadRequest("Item size not found");
            }
            return Success(response, "Item size retrieved successfully");
        }

        [HttpPost]
        public async Task<IActionResult> Index(ItemSizeRequest request)
        {
            string? loggedUser = UserId;
            var result = await _itemSizeService.CreateItemSize(request, loggedUser);
            if (!result) return BadRequest("Item size creation failed");
            return Success(result, "Item size created successfully");
        }

        [HttpPut]
        [Route("{itemSizeId}")]
        public async Task<IActionResult> UpdateItemSize(Guid itemSizeId, ItemSizeRequest request)
        {
            string? loggedUser = UserId;
            var result = await _itemSizeService.UpdateItemSize(itemSizeId, request, loggedUser);
            if (!result) return BadRequest("Item size update failed");
            return Success(result, "Item size updated successfully");
        }

        [HttpPost]
        [Route("delete/{itemSizeId}")]
        public async Task<IActionResult> DeleteItemSize(Guid itemSizeId)
        {
            string? loggedUser = UserId;
            var result = await _itemSizeService.DeleteItemSize(itemSizeId, loggedUser);
            if (!result) return BadRequest("Item size deletion failed");
            return Success(result, "Item size deleted successfully");
        }
    }
}