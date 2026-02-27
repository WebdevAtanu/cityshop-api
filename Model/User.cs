using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace cityshop_api.Model
{
    [Table("M_User")]
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserPhone { get; set; }
        public string? UserEmail { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; } = "User";
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public bool? IsActive { get; set; } = true;
    }
}
