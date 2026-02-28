using System.ComponentModel.DataAnnotations;

namespace cityshop_api.DTO
{
    public class ShopResponse
    {
        public Guid ShopId { get; set; }
        public string? ShopName { get; set; }
        public string? ShopAddress { get; set; }
        public string? Pincode { get; set; }
        public string? ShopPhone { get; set; }
        public string? ShopLogo { get; set; }
        public string? ShopImage { get; set; }
        public string? GstNo { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public Guid? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? Prefix { get; set; }
        public string? Colour { get; set; }
        public Guid? ShopTypeId { get; set; }
        public string? TypeName { get; set; }
        public TimeSpan? OpeningTime { get; set; }
        public TimeSpan? ClosingTime { get; set; }
        public string? NearByLocation { get; set; }
        public DateTime? CreatedDate { get; set; } 
        public string? CreatedBy { get; set; }
        public DateTime? DLM { get; set; }
        public string? ULM { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
