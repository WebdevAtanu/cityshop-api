namespace cityshop_api.DTO
{
    public class OrderRequest
    {
        public Guid ShopId { get; set; }
        public List<OrderedItem>? OrderedItems { get; set; }
        public OrderBy? OrderBy { get; set; }
    }
}