namespace cityshop_api.DTO
{
    public class ShopTypeRequest
    {
        public string? TypeName { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
