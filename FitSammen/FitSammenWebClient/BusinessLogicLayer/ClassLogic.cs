using FitSammenWebClient.Models;
using FitSammenWebClient.ServiceLayer;

namespace FitSammenWebClient.BusinessLogicLayer
{
    public class ClassLogic : IClassLogic
    {
        private readonly IClassAccess _classService;
        public ClassLogic(IClassAccess classAccess)
        {
            _classService = classAccess;
        }

        public async Task<MemberBookingResponse?> SignUpAMember(int member, int theClass, string token)
        {
            return await _classService.SignUpMemberToClassAsync(member, theClass, token);
        }

        public async Task<IEnumerable<Class>?> GetAllClassesAsync(int id = -1)
        {
            IEnumerable<Class>? allClasses = null;

            try
            {
                allClasses = await _classService.GetClasses(id);

                if (allClasses != null)
                {
                    return allClasses;
                }
            }
            catch (Exception)
            {
                allClasses = null;
            }
            return allClasses;
        }
    }
}
