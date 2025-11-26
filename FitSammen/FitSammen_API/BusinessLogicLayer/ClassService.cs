using FitSammen_API.DatabaseAccessLayer;
using FitSammen_API.DTOs;
using FitSammen_API.Exceptions;
using FitSammen_API.Mapping;
using FitSammen_API.Model;

namespace FitSammen_API.BusinessLogicLayer
{
    public class ClassService : IClassService
    {
        private readonly IClassAccess _classAccess;

        public ClassService(IClassAccess classAccess)
        {
            _classAccess = classAccess;
        }

        public BookingClassResult CreateClass(Class cls)
        {
            BookingClassResult result;
            if(cls.TrainingDate < DateOnly.FromDateTime(DateTime.Now) || 
                cls.StartTime < TimeOnly.Parse("7:00:00") || 
                cls.StartTime > TimeOnly.Parse("17:00:00"))
            {
                return new BookingClassResult
                {
                    Status = BookingClassStatus.Conflict,
                    ClassId = null
                };
            }
            try
            {
                int res = _classAccess.CreateClass(cls);
                switch (res)
                {
                    case 0:
                        result = new BookingClassResult
                        {
                            Status = BookingClassStatus.Conflict,
                            ClassId = null
                        };
                        break;
                    default:
                        result = new BookingClassResult
                        {
                            Status = BookingClassStatus.Success,
                            ClassId = res
                        };
                        break;
                }
            }
            catch (DataAccessException)
            {
                result = new BookingClassResult
                {
                    Status = BookingClassStatus.Error,
                    ClassId = null
                };
            }
            return result;
        }

        public IEnumerable<Model.Class> GetUpcomingClasses()
        {
            try
            {
                return _classAccess.GetUpcomingClasses();
            }
            catch (DataAccessException)
            {
                throw;
            }

        }
    }
}
