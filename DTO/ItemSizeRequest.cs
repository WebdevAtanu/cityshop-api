namespace cityshop_api.DTO
{
    public class ItemSizeRequest
    {
        public string? SizeName { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}