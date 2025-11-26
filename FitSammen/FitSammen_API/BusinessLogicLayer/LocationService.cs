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

        public IEnumerable<LocationListDTO> GetAllLocations()
        {
            try
            {
                List<LocationListDTO> locationsDTO = new List<LocationListDTO>();
                IEnumerable<Location> locations = _classAccess.GetAllLocations();
                foreach (Location loc in locations)
                {
                    locationsDTO.Add(ModelConversion.LocationToLocationListDTO(loc));
                }
                return locationsDTO;
            }
            catch (DataAccessException)
            {
                throw new DataAccessException("Error retrieving locations from the database.");
            }
        }

        public IEnumerable<EmployeeListDTO> GetEmployeesByLocationId(int locationId)
        {
            try
            {
                List<EmployeeListDTO> employeesDTO = new List<EmployeeListDTO>();
                IEnumerable<Employee> employees = _classAccess.GetEmployeesByLocationId(locationId);
                foreach (Employee e in employees)
                {
                    employeesDTO.Add(ModelConversion.EmployeeToEmployeeListDTO(e));
                }
                return employeesDTO;
            }
            catch (DataAccessException)
            {
                throw new DataAccessException("Error retrieving employees from the database for the specified location.");
            }
        }

        public IEnumerable<RoomListDTO> GetRoomsByLocationId(int locationId)
        {
            try
            {
                List<RoomListDTO> roomsDTO = new List<RoomListDTO>();
                IEnumerable<Room> rooms = _classAccess.GetRoomsByLocationId(locationId);
                foreach (Room r in rooms)
                {
                    roomsDTO.Add(ModelConversion.RoomToRoomListDTO(r));
                }
                return roomsDTO;
            }
            catch (DataAccessException)
            {
                throw new DataAccessException("Error retrieving rooms from the database for the specified location.");
            }
        }
    }
}
