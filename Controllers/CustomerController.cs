using cityshop_api.DTO;
using cityshop_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cityshop_api.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _customerService.GetAllCustomer();
            if (response == null)
            {
                return BadRequest("No customers found");
            }
            return Success(response, "Customers retrieved successfully");
        }

        [HttpGet]
        [Route("{customerId}")]
        public async Task<IActionResult> GetById(Guid customerId)
        {
            var response = await _customerService.GetCustomerById(customerId);
            if (response == null)
            {
                return BadRequest("Customer not found");
            }
            return Success(response, "Customer retrieved successfully");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerRequest customerRequest)
        {
            string? loggedUser = UserId;
            var response = await _customerService.CreateCustomer(customerRequest, loggedUser);
            if (!response)
            {
                return BadRequest("Customer creation failed");
            }
            return Success(response, "Customer created successfully");
        }

        [HttpPut]
        [Route("{customerId}")]
        public async Task<IActionResult> Update(Guid customerId, CustomerRequest customerRequest)
        {
            string? loggedUser = UserId;
            var response = await _customerService.UpdateCustomer(customerId, customerRequest, loggedUser);
            if (!response)
            {
                return BadRequest("Customer update failed");
            }
            return Success(response, "Customer updated successfully");
        }

        [HttpPost]
        [Route("delete/{customerId}")]
        public async Task<IActionResult> Delete(Guid customerId)
        {
            string? loggedUser = UserId;
            var response = await _customerService.DeleteCustomer(customerId, loggedUser);
            if (!response)
            {
                return BadRequest("Customer deletion failed");
            }
            return Success(response, "Customer deleted successfully");
        }
    }
}