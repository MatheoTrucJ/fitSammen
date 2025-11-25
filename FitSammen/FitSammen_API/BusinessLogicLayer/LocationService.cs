using FitSammen_API.DatabaseAccessLayer;

namespace FitSammen_API.BusinessLogicLayer
{
    public class LocationService
    {
        private readonly IClassAccess _classAccess;

        public LocationService(IClassAccess classAccess)
        {
            _classAccess = classAccess;
        }
    }
}
