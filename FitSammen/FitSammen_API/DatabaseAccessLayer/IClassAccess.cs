using FitSammen_API.BusinessLogicLayer;
using FitSammen_API.DTOs;
using FitSammen_API.Model;

namespace FitSammen_API.DatabaseAccessLayer
{
    public interface IClassAccess
    {
        public IEnumerable<Class> GetUpcomingClasses();
        public IEnumerable<Location> GetAllLocations();
        public IEnumerable<Room> GetRoomsByLocationId(int LocationId);
        public IEnumerable<Employee> GetEmployeesByLocationId(int LocationId);
        public int CreateClass(Class cls);
    }
}
