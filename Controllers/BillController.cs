//using cityshop_api.DTO;
//using cityshop_api.Helpers;
//using cityshop_api.Interfaces;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace cityshop_api.Controllers
//{
//    public class BillController : BaseController
//    {
//        private readonly IBillService _billService;
//        public BillController(IBillService billService)
//        {
//            _billService = billService;
//        }

//        [HttpPost]
//        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
//        public async Task<IActionResult> Index(BillRequest billRequest)
//        {
//            var response = await _billService.CreateOrder(orderRequest);
//            if (response == null)
//            {
//                return BadRequest("Order request failed");
//            }
//            return Success(response);
//        }

//        [HttpGet("{shopId}")]
//        [ProducesResponseType(typeof(ApiResponse<OrderResponse>), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
//        public async Task<IActionResult> Index(Guid shopId)
//        {
//            var response = await _billService.GetAllOrders(shopId);
//            if (response == null)
//            {
//                return BadRequest("Orders not found");
//            }
//            return Success(response, "Orders found");
//        }

//        [HttpPut("updateOrder/{orderNo}")]
//        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
//        public async Task<IActionResult> UpdateOrder(string orderNo, OrderRequest orderRequest)
//        {
//            var response = await _billService.UpdateOrder(orderNo, orderRequest);
//            if (!response)
//            {
//                return BadRequest("Order update failed");
//            }
//            return Success(response, "Order updated successfully");
//        }
//    }
//}
