namespace cityshop_api.DTO
{
    public class ShopStatusResponse
    {
        public Guid StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? Prefix { get; set; }
        public string? Colour { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DLM { get; set; }
        public string? ULM { get; set; }
        public bool? IsActive { get; set; }
    }
}