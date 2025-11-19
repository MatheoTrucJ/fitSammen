using FitSammenWebClient.Models;

namespace FitSammenWebClient.ServiceLayer
{
    public interface IClassAccess
    {
        Task<IEnumerable<Class>?> GetClasses(int id = -1);

        Task<MemberBookingResponse> SignUpMemberToClass(int userNumber, int ClassId);

    }
}
