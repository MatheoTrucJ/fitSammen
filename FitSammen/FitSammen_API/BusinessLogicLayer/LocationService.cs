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

        public IEnumerable<LocationDTO> GetAllLocations()
        {
            try
            {
                List<LocationDTO> locationsDTO = new List<LocationDTO>();
                IEnumerable<Location> locations = _classAccess.GetAllLocations();
                foreach (Location loc in locations)
                {
                    locationsDTO.Add(ModelConversion.LocationToLocationDTO(loc));
                }
                return locationsDTO;
            }
            catch (DataAccessException)
            {
                throw new DataAccessException("Error retrieving locations from the database.");
            }
        }

        public IEnumerable<RoomDTO> GetRoomsByLocationId(int locationId)
        {
            try
            {
                List<RoomDTO> roomsDTO = new List<RoomDTO>();
                IEnumerable<Room> rooms = _classAccess.GetRoomsByLocationId(locationId);
                foreach (Room r in rooms)
                {
                    roomsDTO.Add(ModelConversion.RoomToDTO(r));
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
