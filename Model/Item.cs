using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace cityshop_api.Model
{
    [Table("M_Item")]
    public class Item
    {
        [Key]
        public Guid ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public Guid? ItemGroupId { get; set; }
        public Guid? ItemCategoryId { get; set; }
        public Guid? ItemSizeId { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemImage { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? DLM { get; set; }
        public string? ULM { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}