using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface IOrderRepository
    {
        public Task<string> CreateOrder(OrderRequest orderRequest);
        public Task<List<OrderResponse>> GetAllOrders(Guid shopId);
        public Task<bool> UpdateOrder(string orderNo, OrderRequest orderRequest);
    }
}