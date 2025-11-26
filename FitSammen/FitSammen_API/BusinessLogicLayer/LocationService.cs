using FitSammen_API.DatabaseAccessLayer;
using FitSammen_API.DTOs;
using FitSammen_API.Exceptions;
using FitSammen_API.Mapping;
using FitSammen_API.Model;

namespace FitSammen_API.BusinessLogicLayer
{
    public class LocationService : ILocationService
    {
        private readonly IClassAccess _classAccess;

        public LocationService(IClassAccess classAccess)
        {
            _classAccess = classAccess;
        }

        public IEnumerable<Location> GetAllLocations()
        {
            try
            {
                IEnumerable<Location> locations = _classAccess.GetAllLocations();
                return locations;
                
            }
            catch (DataAccessException)
            {
                throw new DataAccessException("Error retrieving locations from the database.");
            }
        }

        public IEnumerable<Employee> GetEmployeesByLocationId(int locationId)
        {
            try
            {
                return _classAccess.GetEmployeesByLocationId(locationId);
            }
            catch (DataAccessException)
            {
                throw new DataAccessException("Error retrieving employees from the database for the specified location.");
            }
        }

        public IEnumerable<Room> GetRoomsByLocationId(int locationId)
        {
            try
            {
                IEnumerable<Room> rooms = _classAccess.GetRoomsByLocationId(locationId);
                return rooms;
            }
            catch (DataAccessException)
            {
                throw new DataAccessException("Error retrieving rooms from the database for the specified location.");
            }
        }
    }
}
