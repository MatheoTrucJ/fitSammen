using FitSammen_API.DTOs;
using FitSammen_API.Model;

namespace FitSammen_API.BusinessLogicLayer
{
    public interface IClassService
    {
        public IEnumerable<Class> GetUpcomingClasses();
        public BookingClassResult CreateClass(ClassCreateRequestDTO cls);
    }
}
