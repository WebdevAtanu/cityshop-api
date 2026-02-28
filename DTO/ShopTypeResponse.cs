using System.ComponentModel.DataAnnotations;

namespace cityshop_api.DTO
{
    public class ShopTypeResponse
    {
        public Guid ShopTypeId { get; set; }
        public string? TypeName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DLM { get; set; }
        public string? ULM { get; set; }
        public bool? IsActive { get; set; }
    }
}
