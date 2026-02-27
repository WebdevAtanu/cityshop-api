using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace cityshop_api.Model
{
    [Table("Map_Order_Bill")]
    public class OrderBill
    {
        [Key]
        public Guid MapId { get; set; }
        public Guid? ShopId { get; set; }
        public string? InvoiceNumber { get; set; }
        public Guid? OrderId { get; set; }
        public decimal? DeliveryCharge { get; set; }
        public decimal? OrderTotal { get; set; }
        public decimal? NetTotal { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? DLM { get; set; }
        public string? ULM { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}