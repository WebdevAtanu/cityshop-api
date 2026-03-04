namespace cityshop_api.DTO
{
    public class ShopItemMapRequest
    {
        public Guid? ShopId { get; set; }
        public List<ShopItems>? ShopItems { get; set; }
    }

    public class ShopItems
    {
        public Guid? ItemId { get; set; }
        public Guid? SizeId { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public int? StockQty { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
