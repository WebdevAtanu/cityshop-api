using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace cityshop_api.Model
{
    [Table("M_ShopType")]
    public class ShopType
    {
        [Key]
        public Guid ShopTypeId { get; set; }
        public string? TypeName { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? DLM { get; set; }
        public string? ULM { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
