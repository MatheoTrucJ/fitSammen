using FitSammenDekstopClient.Model;
using FitSammenDekstopClient.ServiceLayer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSammenDekstopClient.BusinessLogicLayer
{
    public class LocationLogic
    {
        private readonly ILocationService _locationService;

        public LocationLogic(IConfiguration inConfiguration)
        {
            _locationService = new LocationService(inConfiguration);
        }

        public async Task<IEnumerable<Location>?> GetAllLocationsAsync()
        {
            var locationList = await _locationService.GetAllLocationsAsync();
            return locationList?.ToList() ?? new List<Location>();
        }

        public async Task<IEnumerable<Employee>?> GetAllEmployeesFromLocationIdAsync(int locationId)
        {
            var employeeList = await _locationService.GetAllEmployeesFromLocationIdAsync(locationId);
            return employeeList?.ToList() ?? new List<Employee>();
        }

        public async Task<IEnumerable<Room>?> GetAllRoomsFromLocationIdAsync(int locationId)
        {
            var roomList = await _locationService.GetAllRoomsFromLocationIdAsync(locationId);
            return roomList?.ToList() ?? new List<Room>();
        }
    }
}
