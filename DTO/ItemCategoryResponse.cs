namespace cityshop_api.DTO
{
    public class ItemCategoryResponse
    {
        public Guid ItemCategoryId { get; set; }
        public string? CategoryName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DLM { get; set; }
        public string? ULM { get; set; }
        public bool? IsActive { get; set; }
    }
}