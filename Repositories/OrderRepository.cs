using cityshop_api.DTO;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.EntityFrameworkCore;

namespace cityshop_api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<string> CreateOrder(OrderRequest orderRequest)
        {
            return "";
        }

        public async Task<List<OrderResponse>> GetAllOrders(Guid shopId)
        {
            return null;
        }
    }
}