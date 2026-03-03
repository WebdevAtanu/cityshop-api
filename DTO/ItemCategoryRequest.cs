namespace cityshop_api.DTO
{
    public class ItemCategoryRequest
    {
        public string? CategoryName { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}