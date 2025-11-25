using FitSammen_API.Model;

namespace FitSammen_API.DatabaseAccessLayer
{
    public interface IClassAccess
    {
        public IEnumerable<Class> GetUpcomingClasses();
        public IEnumerable<Location> GetAllLocations();
        public IEnumerable<Room> GetRoomsByLocationId(int LocationId);
    }
}
