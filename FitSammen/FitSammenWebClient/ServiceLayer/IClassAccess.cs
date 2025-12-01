using FitSammenWebClient.Models;

namespace FitSammenWebClient.ServiceLayer
{
    public interface IClassAccess
    {
        Task<IEnumerable<Class>?> GetClasses(int id = -1);

        Task<MemberBookingResponse> SignUpMemberToClassAsync(int userNumber, int ClassId, string token);

    }
}
