namespace cityshop_api.DTO
{
    public class ItemGroupResponse
    {
        public Guid ItemGroupId { get; set; }
        public string? GroupName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DLM { get; set; }
        public string? ULM { get; set; }
        public bool? IsActive { get; set; }
    }
}