namespace FitSammen_API.Model
{
    public class Location
    {
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public Zipcode Zipcode { get; set; }
        public int LocationId { get; set; }

        public Location(string streetName, int housenumber, int zipCodeNumber, string cityName, string countryName)
        {
            StreetName = streetName;
            HouseNumber = housenumber;
            Country country = new Country { CountryName = countryName };
            City city = new City { CityName = cityName, Country = country };
            Zipcode zipcode = new Zipcode { ZipcodeNumber = zipCodeNumber, City = city };
            Zipcode = zipcode;
            
        }

        public Location()
        {
        }

    }

    public class Zipcode
    {
        public int ZipcodeNumber { get; set; }
        public City City { get; set; }
    }

    public class City
    {
        public string CityName { get; set; }
        public Country? Country { get; set; }
    }

    public class Country
    {
        public string CountryName { get; set; }
    }
}
