namespace cityshop_api.DTO
{
    public class ItemResponse
    {
        public Guid ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public Guid? ItemGroupId { get; set; }
        public string? ItemgroupName { get; set; }
        public Guid? ItemCategoryId { get; set; }
        public string? ItemCategoryName { get; set; }
        public Guid? ItemSizeId { get; set; }
        public string? ItemSizeName { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemImage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DLM { get; set; }
        public string? ULM { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}