using FitSammen_API.DTOs;

namespace FitSammen_API.BusinessLogicLayer
{
    public interface ILocationService
    {
        public IEnumerable<LocationListDTO> GetAllLocations();
        public IEnumerable<RoomListDTO> GetRoomsByLocationId(int locationId);
        public IEnumerable<EmployeeListDTO> GetEmployeesByLocationId(int locationId);
    }
}
