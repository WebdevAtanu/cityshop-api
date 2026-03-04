using System.ComponentModel.DataAnnotations;

namespace cityshop_api.DTO
{
    public class ShopItemMapResponse
    {
        public Guid? ShopId { get; set; }
        public string? ShopName { get; set; }
        public List<MapItem>? MapItems { get; set; }
    }

    public class MapItem
    {
        public Guid? ItemId { get; set; }
        public string? ItemName { get; set; }
        public Guid? SizeId { get; set; }
        public string? SizeName { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public int? StockQty { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
