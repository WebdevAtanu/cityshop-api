namespace cityshop_api.DTO
{
    public class ShopRequest
    {
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
        public Guid? ShopTypeId { get; set; }
        public TimeSpan? OpeningTime { get; set; }
        public TimeSpan? ClosingTime { get; set; }
        public string? NearByLocation { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
