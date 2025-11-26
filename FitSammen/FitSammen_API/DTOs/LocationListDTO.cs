namespace FitSammen_API.DTOs
{
    public class LocationListDTO
    {
        public int LocationId { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public ZipcodeDTO Zipcode { get; set; }

        public LocationListDTO(int locationId, string streetName, int houseNumber, int zipcode, string cityName, string country)
        {
            LocationId = locationId;
            StreetName = streetName;
            HouseNumber = houseNumber;
            ZipcodeDTO zDTO = new ZipcodeDTO
            {
                ZipcodeNumber = zipcode,
                City = new CityDTO
                {
                    CityName = cityName,
                    Country = new CountryDTO
                    {
                        CountryName = country
                    }
                }
            };
        }

        public LocationListDTO()
        {
        }
    }

    public class ZipcodeDTO
    {
        public int ZipcodeNumber { get; set; }
        public CityDTO City { get; set; }
    }

    public class CityDTO
    {
        public string CityName { get; set; }
        public CountryDTO Country { get; set; }
    }

    public class CountryDTO
    {
        public string CountryName { get; set; }
    }
}




