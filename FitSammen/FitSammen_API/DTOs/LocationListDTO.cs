namespace FitSammen_API.DTOs
{
    public class LocationListDTO
    {
        public int LocationId { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public string CityName { get; set; }

        public LocationListDTO(int locationId, string streetName, int houseNumber, string cityName)
        {
            LocationId = locationId;
            StreetName = streetName;
            HouseNumber = houseNumber;
            CityName = cityName;
        }
    }
}
