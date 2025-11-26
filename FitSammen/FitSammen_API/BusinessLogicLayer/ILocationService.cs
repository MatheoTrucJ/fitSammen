using FitSammen_API.DTOs;
using FitSammen_API.Model;

namespace FitSammen_API.BusinessLogicLayer
{
    public interface ILocationService
    {
        public IEnumerable<Location> GetAllLocations();
        public IEnumerable<Room> GetRoomsByLocationId(int locationId);
        public IEnumerable<Employee> GetEmployeesByLocationId(int locationId);
    }
}
