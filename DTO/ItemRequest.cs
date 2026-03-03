namespace cityshop_api.DTO
{
    public class ItemRequest
    {
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public Guid? ItemGroupId { get; set; }
        public Guid? ItemCategoryId { get; set; }
        public Guid? ItemSizeId { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemImage { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}