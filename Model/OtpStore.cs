using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace cityshop_api.Model
{
    [Table("Tbl_OtpStore")]
    public class OtpStore
    {
        [Key]
        public Guid TrackId { get; set; }
        public string? Email { get; set; }
        public string? Otp { get; set; }
        public DateTime? createTime { get; set; }
        public DateTime? ExpiryTime { get; set; }
        public bool? IsUsed { get; set; }
    }
}