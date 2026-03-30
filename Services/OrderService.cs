using cityshop_api.DTO;
using cityshop_api.Interfaces;

namespace cityshop_api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<string> CreateOrder(OrderRequest orderRequest)
        {
            return await _orderRepository.CreateOrder(orderRequest);
        }

        public async Task<List<OrderResponse>> GetAllOrders(Guid shopId)
        {
            return await _orderRepository.GetAllOrders(shopId);
        }

        public async Task<bool> UpdateOrder(string orderNo, OrderRequest orderRequest)
        {
            return await _orderRepository.UpdateOrder(orderNo, orderRequest);
        }
    }
}