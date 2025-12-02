using FitSammenWebClient.Models;

namespace FitSammenWebClient.BusinessLogicLayer
{
    public interface IClassLogic
    {
       Task<MemberBookingResponse?> SignUpAMember(int theClass, string token);
        Task<IEnumerable<Class>?> GetAllClassesAsync(int id = -1);

    }
}
