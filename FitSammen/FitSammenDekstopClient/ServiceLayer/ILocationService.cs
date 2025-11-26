using FitSammenDekstopClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSammenDekstopClient.ServiceLayer
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>?> GetAllLocationsAsync();
        Task<IEnumerable<Room>?> GetAllRoomsFromLocationIdAsync(int locationId);
        Task<IEnumerable<Employee>?> GetAllEmployeesFromLocationIdAsync(int locationId);
    }
}
