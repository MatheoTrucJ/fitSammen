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
            throw new NotImplementedException();
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
