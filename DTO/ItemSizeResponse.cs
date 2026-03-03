namespace cityshop_api.DTO
{
    public class ItemSizeResponse
    {
        public Guid ItemSizeId { get; set; }
        public string? SizeName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DLM { get; set; }
        public string? ULM { get; set; }
        public bool? IsActive { get; set; }
    }
}