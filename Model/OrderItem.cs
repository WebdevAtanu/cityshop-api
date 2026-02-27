using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace cityshop_api.Model
{
    [Table("Map_Order_Items")]
    public class OrderItem
    {
        [Key]
        public Guid MapId { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ItemId { get; set; }
        public int? ItemQty { get; set; }
        public decimal? ItemRate { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? DLM { get; set; }
        public string? ULM { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}