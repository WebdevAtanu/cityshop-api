using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface ICustomerService
    {
        public Task<List<CustomerResponse>> GetAllCustomer();
        public Task<CustomerResponse?> GetCustomerById(Guid customerId);
        public Task<bool> CreateCustomer(CustomerRequest customerRequest, string loggedUser);
        public Task<bool> UpdateCustomer(Guid customerId, CustomerRequest customerRequest, string loggedUser);
        public Task<bool> DeleteCustomer(Guid customerId, string loggedUser);
    }
}