using cityshop_api.DTO;
using cityshop_api.Helpers;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cityshop_api.Controllers
{
    public class ShopTypeController : BaseController
    {
        private readonly IShopTypeService _shopTypeService;
        public ShopTypeController(IShopTypeService shopTypeService)
        {
            _shopTypeService = shopTypeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<ShopResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Index()
        {
            var response = await _shopTypeService.GetAllShopType();
            if (response == null)
            {
                return Success("No shop types found");
            }
            return Success(response, "Shop types retrieved successfully");
        }

        [HttpGet]
        [Route("{shopTypeId}")]
        [ProducesResponseType(typeof(ApiResponse<ShopResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid shopTypeId)
        {
            var response = await _shopTypeService.GetShopTypeById(shopTypeId);
            if (response == null)
            {
                return BadRequest("Shop type not found");
            }
            return Success(response, "Shop type retrieved successfully");
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(ShopTypeRequest shopTypeRequest)
        {
            string? loggedUser = UserId;
            var response = await _shopTypeService.CreateShopType(shopTypeRequest, loggedUser);
            if (!response)
            {
                return BadRequest("Shop type creation failed");
            }
            return Success(response, "Shop type created successfully");
        }

        [HttpPut]
        [Route("{shopTypeId}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid shopTypeId, ShopTypeRequest shopTypeRequest)
        {
            string? loggedUser = UserId;
            var response = await _shopTypeService.UpdateShopType(shopTypeId, shopTypeRequest, loggedUser);
            if (!response)
            {
                return BadRequest("Shop type update failed");
            }
            return Success(response, "Shop type updated successfully");
        }

        [HttpPost]
        [Route("delete/{shopTypeId}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid shopTypeId)
        {
            string? loggedUser = UserId;
            var response = await _shopTypeService.DeleteShopType(shopTypeId, loggedUser);
            if (!response)
            {
                return BadRequest("Shop type deletion failed");
            }
            return Success(response, "Shop type deleted successfully");
        }
    }
}
