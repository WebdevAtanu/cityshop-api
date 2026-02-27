using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace cityshop_api.Model
{
    [Table("M_ShopStatus")]
    public class ShopStatus
    {
        [Key]
        public Guid StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? Prefix { get; set; }
        public string? Colour { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? DLM { get; set; }
        public string? ULM { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
