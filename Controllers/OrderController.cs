using cityshop_api.DTO;
using cityshop_api.Helpers;
using cityshop_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cityshop_api.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Index(OrderRequest orderRequest)
        {
            var response = await _orderService.CreateOrder(orderRequest);
            if (response == null)
            {
                return BadRequest("Order request failed");
            }
            return Success(response);
        }

        [HttpGet("{shopId}")]
        [ProducesResponseType(typeof(ApiResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Index(Guid shopId)
        {
            var response = await _orderService.GetAllOrders(shopId);
            if (response == null)
            {
                return BadRequest("Orders not found");
            }
            return Success(response, "Orders found");
        }
    }
}
