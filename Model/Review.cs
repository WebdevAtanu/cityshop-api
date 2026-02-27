using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace cityshop_api.Model
{
    [Table("Tbl_Reviews")]
    public class Review
    {
        [Key]
        public Guid MapId { get; set; }
        public Guid? ShopId { get; set; }
        public Guid? CustomerId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
    }
}