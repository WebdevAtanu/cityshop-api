using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace cityshop_api.Model
{
    [Table("Tbl_ActiveUser")]
    public class ActiveUser
    {
        [Key]
        public Guid MapId { get; set; }
        public Guid? UserId { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? LoginDateTime { get; set; }
    }
}