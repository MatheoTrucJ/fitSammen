namespace FitSammen_API.DTOs
{
    public class LocationDTO
    {
        public int? LocationId { get; set; } = null;
        public string? StreetName { get; set; } = null;
        public int? HouseNumber { get; set; } = null;
        public int? ZipcodeNumber { get; set; } = null;
        public string? CityName { get; set; } = null;
        public string? CountryName { get; set; } = null;
    }
}
