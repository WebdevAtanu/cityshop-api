using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace cityshop_api.Model
{
    [Table("M_ItemSize")]
    public class ItemSize
    {
        [Key]
        public Guid SizeId { get; set; }
        public string? SizeName { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? DLM { get; set; }
        public string? ULM { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}