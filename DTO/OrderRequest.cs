using System.ComponentModel.DataAnnotations;

namespace cityshop_api.DTO
{
    public class OrderRequest
    {
        public Guid ShopId { get; set; }
        public List<OrderedItem>? OrderedItems { get; set; }
        public OrderBy? OrderBy { get; set; }
    }

    public class OrderBy
    {
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }
        [Required]
        [Phone]
        [StringLength(20)]
        public string? CustomerPhone { get; set; }
        [EmailAddress]
        public string? CustomerEmail { get; set; }
        [Required]
        [MinLength(10)]
        public string? CustomerAddress { get; set; }
        public string? Pincode { get; set; }
    }
}