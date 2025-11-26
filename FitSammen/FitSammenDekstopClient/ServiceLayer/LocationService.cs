using FitSammenDekstopClient.Model;
using FitSammenDesktopClient.ServiceLayer;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSammenDekstopClient.ServiceLayer
{
    public class LocationService : ServiceConnection, ILocationService
    {
        public LocationService(IConfiguration inBaseUrl) : base(inBaseUrl["ServiceUrlToUse"]) { }

        public async Task<IEnumerable<Location>?> GetAllLocationsAsync()
        {
            List<Location>? result = new List<Location>();
            UseUrl = BaseUrl + "locations";

            try
            {
                HttpResponseMessage? response = await CallServiceGet();

                if (response != null && response.IsSuccessStatusCode)
                {
                    string? content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<Location>>(content);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<IEnumerable<Employee>?> GetAllEmployeesFromLocationIdAsync(int locationId)
        {
            List<Employee>? result = new List<Employee>();
            UseUrl = BaseUrl + $"locations/{locationId}/employees";

            try
            {
                HttpResponseMessage? response = await CallServiceGet();
                if (response != null && response.IsSuccessStatusCode)
                {
                    string? content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<Employee>>(content);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<IEnumerable<Room>?> GetAllRoomsFromLocationIdAsync(int locationId)
        {
            List<Room>? result = new List<Room>();
            UseUrl = BaseUrl + $"locations/{locationId}/rooms";

            try
            {
                HttpResponseMessage? response = await CallServiceGet();
 
                if (response != null && response.IsSuccessStatusCode)
                {
                    string? content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<Room>>(content);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
