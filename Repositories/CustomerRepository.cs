using cityshop_api.DTO;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.EntityFrameworkCore;

namespace cityshop_api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDBContext _context;
        public CustomerRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerResponse>> GetAllCustomer()
        {
            return await _context.Customers
                .Select(c => new CustomerResponse
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    CustomerPhone = c.CustomerPhone,
                    CustomerEmail = c.CustomerEmail,
                    CustomerAddress = c.CustomerAddress,
                    Pincode = c.Pincode,
                    CreatedDate = c.CreatedDate,
                    CreatedBy = c.CreatedBy,
                    DLM = c.DLM,
                    ULM = c.ULM,
                    IsActive = c.IsActive
                }).ToListAsync();
        }

        public async Task<CustomerResponse?> GetCustomerById(Guid customerId)
        {
            return await _context.Customers
                .Where(c => c.CustomerId == customerId && c.IsActive == true)
                .Select(c => new CustomerResponse
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    CustomerPhone = c.CustomerPhone,
                    CustomerEmail = c.CustomerEmail,
                    CustomerAddress = c.CustomerAddress,
                    Pincode = c.Pincode,
                    CreatedDate = c.CreatedDate,
                    CreatedBy = c.CreatedBy,
                    DLM = c.DLM,
                    ULM = c.ULM,
                    IsActive = c.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<CustomerResponse?> GetCustomerByPhone(string customerPhone)
        {
            return await _context.Customers
                .Where(c => c.CustomerPhone.ToLower() == customerPhone.ToLower() && c.IsActive == true)
                .Select(c => new CustomerResponse
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    CustomerPhone = c.CustomerPhone,
                    CustomerEmail = c.CustomerEmail,
                    CustomerAddress = c.CustomerAddress,
                    Pincode = c.Pincode,
                    CreatedDate = c.CreatedDate,
                    CreatedBy = c.CreatedBy,
                    DLM = c.DLM,
                    ULM = c.ULM,
                    IsActive = c.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateCustomer(CustomerRequest customerRequest, string loggedUser)
        {
            await _context.Customers.AddAsync(new Customer
            {
                CustomerId = Guid.NewGuid(),
                CustomerName = customerRequest.CustomerName,
                CustomerPhone = customerRequest.CustomerPhone,
                CustomerEmail = customerRequest.CustomerEmail,
                CustomerAddress = customerRequest.CustomerAddress,
                Pincode = customerRequest.Pincode,
                Password = customerRequest.Password,
                CreatedBy = loggedUser,
                CreatedDate = DateTime.Now,
                IsActive = true
            });
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateCustomer(Guid customerId, CustomerRequest customerRequest, string loggedUser)
        {
            var existing = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId)
                           ?? throw new Exception("Customer not found");
            existing.CustomerName = customerRequest.CustomerName;
            existing.CustomerPhone = customerRequest.CustomerPhone;
            existing.CustomerEmail = customerRequest.CustomerEmail;
            existing.CustomerAddress = customerRequest.CustomerAddress;
            existing.Pincode = customerRequest.Pincode;
            if (!string.IsNullOrEmpty(customerRequest.Password))
            {
                existing.Password = customerRequest.Password;
            }
            existing.DLM = DateTime.Now;
            existing.ULM = loggedUser;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteCustomer(Guid customerId, string loggedUser)
        {
            var existing = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId)
                           ?? throw new Exception("Customer not found");
            existing.IsActive = false;
            existing.DLM = DateTime.Now;
            existing.ULM = loggedUser;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}