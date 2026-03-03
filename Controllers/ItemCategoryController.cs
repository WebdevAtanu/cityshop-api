using cityshop_api.DTO;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cityshop_api.Controllers
{
    public class ItemCategoryController : BaseController
    {
        private readonly IItemCategoryService _itemCategoryService;
        public ItemCategoryController(IItemCategoryService itemCategoryService)
        {
            _itemCategoryService = itemCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _itemCategoryService.GetAllItemCategory();
            if (response == null)
            {
                return BadRequest("No item categories found");
            }
            return Success(response, "Item categories retrieved successfully");
        }

        [HttpGet]
        [Route("{itemCategoryId}")]
        public async Task<IActionResult> GetItemCategoryById(Guid itemCategoryId)
        {
            var response = await _itemCategoryService.GetItemCategoryById(itemCategoryId);
            if (response == null)
            {
                return BadRequest("Item category not found");
            }
            return Success(response, "Item category retrieved successfully");
        }

        [HttpPost]
        public async Task<IActionResult> Index(ItemCategoryRequest request)
        {
            string? loggedUser = UserId;
            var result = await _itemCategoryService.CreateItemCategory(request, loggedUser);
            if (!result) return BadRequest("Item category creation failed");
            return Success(result, "Item category created successfully");
        }

        [HttpPut]
        [Route("{itemCategoryId}")]
        public async Task<IActionResult> UpdateItemCategory(Guid itemCategoryId, ItemCategoryRequest request)
        {
            string? loggedUser = UserId;
            var result = await _itemCategoryService.UpdateItemCategory(itemCategoryId, request, loggedUser);
            if (!result) return BadRequest("Item category update failed");
            return Success(result, "Item category updated successfully");
        }

        [HttpPost]
        [Route("delete/{itemCategoryId}")]
        public async Task<IActionResult> DeleteItemCategory(Guid itemCategoryId)
        {
            string? loggedUser = UserId;
            var result = await _itemCategoryService.DeleteItemCategory(itemCategoryId, loggedUser);
            if (!result) return BadRequest("Item category deletion failed");
            return Success(result, "Item category deleted successfully");
        }
    }
}