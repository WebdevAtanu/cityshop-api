using cityshop_api.DTO;
using cityshop_api.Helpers;
using cityshop_api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cityshop_api.Controllers
{
    public class ShopItemMapController : BaseController
    {
        private readonly IShopItemMapService _shopItemMapService;
        public ShopItemMapController(IShopItemMapService shopItemMapService)
        {
            _shopItemMapService = shopItemMapService;
        }

        [HttpGet]
        [Route("shopId")]
        [ProducesResponseType(typeof(ApiResponse<List<ShopItemMapResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Index(Guid shopId)
        {
            var response = await _shopItemMapService.GetAllShopItems(shopId);
            if (response == null)
            {
                return Success("No mappings found");
            }
            return Success(response, "Shop items retrieved successfully");
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(ShopItemMapRequest shopItemMapRequest)
        {
            string? loggedUser = UserId;
            var response = await _shopItemMapService.MapShopItems(shopItemMapRequest, loggedUser);
            if (!response)
            {
                return BadRequest("Shop mapping creation failed");
            }
            return Success(response, "Shop mapping created successfully");
        }
    }
}
