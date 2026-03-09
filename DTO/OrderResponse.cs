namespace cityshop_api.DTO
{
    public class OrderResponse
    {
        public Guid ShopId { get; set; }
        public string? ShopName { get; set; }
        public Guid ShopTypeId { get; set; }
        public string? TypeName { get; set; }
        public string? ShopAddress { get; set; }
        public string? Pincode { get; set; }
        public string? ShopPhone { get; set; }
        public string? NearByLocation { get; set; }
        public List<OrderedItem>? OrderedItems { get; set; }
        public OrderBy? OrderBy { get; set; }

    }

    public class OrderedItem
    {
        public Guid? ItemId { get; set; }
        public string? ItemName { get; set; }
        public Guid? SizeId { get; set; }
        public string? SizeName { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public bool? IsActive { get; set; } = true;
    }

    public class OrderBy
    {
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerAddress { get; set; }
        public string? Pincode { get; set; }
    }
}