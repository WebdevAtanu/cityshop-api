namespace cityshop_api.DTO
{
    public class ItemGroupRequest
    {
        public string? GroupName { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}