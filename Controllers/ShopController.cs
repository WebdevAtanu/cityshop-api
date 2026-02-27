using cityshop_api.DTO;
using cityshop_api.Helpers;
using cityshop_api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cityshop_api.Controllers
{
    public class ShopController : BaseController
    {
        private readonly IShopService _shopService;
        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<ShopResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Index()
        {
            var response = await _shopService.GetAllShops();
            if (response == null)
            {
                return Success("No shops found");
            }
            return Success(response, "Shops retrieved successfully");
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(ShopRequest shopRequest)
        {
            var response = await _shopService.CreateShop(shopRequest);
            if (!response)
            {
                return BadRequest("Shop creation failed");
            }
            return Success(response, "Shop created successfully");
        }
    }
}
