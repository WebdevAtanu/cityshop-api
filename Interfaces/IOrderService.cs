using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface IOrderService
    {
        public Task<string> CreateOrder(OrderRequest orderRequest);
        public Task<List<OrderResponse>> GetAllOrders(Guid shopId);
    }
}