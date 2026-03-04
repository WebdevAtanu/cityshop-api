namespace cityshop_api.DTO
{
    public class CustomerRequest
    {
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerAddress { get; set; }
        public string? Pincode { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}