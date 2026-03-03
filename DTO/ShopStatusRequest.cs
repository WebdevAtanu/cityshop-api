namespace cityshop_api.DTO
{
    public class ShopStatusRequest
    {
        public string? StatusName { get; set; }
        public string? Prefix { get; set; }
        public string? Colour { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}