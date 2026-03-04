using cityshop_api.DTO;
using cityshop_api.Interfaces;

namespace cityshop_api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerResponse>> GetAllCustomer()
        {
            return await _customerRepository.GetAllCustomer();
        }

        public async Task<CustomerResponse?> GetCustomerById(Guid customerId)
        {
            return await _customerRepository.GetCustomerById(customerId);
        }

        public async Task<bool> CreateCustomer(CustomerRequest customerRequest, string loggedUser)
        {
            if (!string.IsNullOrEmpty(customerRequest.CustomerPhone))
            {
                var existing = await _customerRepository.GetCustomerByPhone(customerRequest.CustomerPhone);
                if (existing is not null)
                {
                    throw new Exception("Customer with the same phone already exists.");
                }
            }
            return await _customerRepository.CreateCustomer(customerRequest, loggedUser);
        }

        public async Task<bool> UpdateCustomer(Guid customerId, CustomerRequest customerRequest, string loggedUser)
        {
            var existing = await _customerRepository.GetCustomerById(customerId);
            if (existing is null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(customerRequest.CustomerPhone) && existing.CustomerPhone != customerRequest.CustomerPhone)
            {
                var response = await _customerRepository.GetCustomerByPhone(customerRequest.CustomerPhone);
                if (response is not null)
                {
                    return false;
                }
            }
            return await _customerRepository.UpdateCustomer(customerId, customerRequest, loggedUser);
        }

        public async Task<bool> DeleteCustomer(Guid customerId, string loggedUser)
        {
            var existing = await _customerRepository.GetCustomerById(customerId);
            if (existing is null)
            {
                return false;
            }
            return await _customerRepository.DeleteCustomer(customerId, loggedUser);
        }
    }
}