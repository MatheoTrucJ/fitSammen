using FitSammen_API.DTOs;

namespace FitSammen_API.BusinessLogicLayer
{
    public interface ILocationService
    {
        public IEnumerable<LocationDTO> GetAllLocations();
        public IEnumerable<RoomDTO> GetRoomsByLocationId(int locationId);
    }
}
