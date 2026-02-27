using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace cityshop_api.Model
{
    [Table("Map_Shop_Item")]
    public class ShopItem
    {
        [Key]
        public Guid MapId { get; set; }
        public Guid? ShopId { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? SizeId { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public int? StockQty { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? DLM { get; set; }
        public string? ULM { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}