using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace cityshop_api.Model
{
    [Table("Tbl_Shop_Order")]
    public class ShopOrder
    {
        [Key]
        public Guid OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public Guid? ShopId { get; set; }
        public Guid? CustomerId { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? Pincode { get; set; }
        public string? Phone { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? DLM { get; set; }
        public string? ULM { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}