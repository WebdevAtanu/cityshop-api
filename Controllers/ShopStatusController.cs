using cityshop_api.DTO;
using cityshop_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cityshop_api.Controllers
{
    public class ShopStatusController : BaseController
    {
        private readonly IShopStatusService _shopStatusService;
        public ShopStatusController(IShopStatusService shopStatusService)
        {
            _shopStatusService = shopStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _shopStatusService.GetAllShopStatus();
            if (response == null)
            {
                return Success("No shop statuses found");
            }
            return Success(response, "Shop statuses retrieved successfully");
        }

        [HttpGet]
        [Route("{statusId}")]
        public async Task<IActionResult> GetById(Guid statusId)
        {
            var response = await _shopStatusService.GetShopStatusById(statusId);
            if (response == null)
            {
                return BadRequest("Shop status not found");
            }
            return Success(response, "Shop status retrieved successfully");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShopStatusRequest shopStatusRequest)
        {
            string? loggedUser = UserId;
            var response = await _shopStatusService.CreateShopStatus(shopStatusRequest, loggedUser);
            if (!response)
            {
                return BadRequest("Shop status creation failed");
            }
            return Success(response, "Shop status created successfully");
        }

        [HttpPut]
        [Route("{statusId}")]
        public async Task<IActionResult> Update(Guid statusId, ShopStatusRequest shopStatusRequest)
        {
            string? loggedUser = UserId;
            var response = await _shopStatusService.UpdateShopStatus(statusId, shopStatusRequest, loggedUser);
            if (!response)
            {
                return BadRequest("Shop status update failed");
            }
            return Success(response, "Shop status updated successfully");
        }

        [HttpPost]
        [Route("delete/{statusId}")]
        public async Task<IActionResult> Delete(Guid statusId)
        {
            string? loggedUser = UserId;
            var response = await _shopStatusService.DeleteShopStatus(statusId, loggedUser);
            if (!response)
            {
                return BadRequest("Shop status deletion failed");
            }
            return Success(response, "Shop status deleted successfully");
        }
    }
}