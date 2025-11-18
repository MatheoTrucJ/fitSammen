using FitSammenWebClient.Models;
using FitSammenWebClient.ServiceLayer;

namespace FitSammenWebClient.BusinessLogicLayer
{
    public class ClassLogic
    {
        private readonly ClassService _classService;
        public ClassLogic(IConfiguration inConfiguration)
        {
            _classService = new ClassService(inConfiguration);
        }

        public async Task<Boolean> signUpAMember(Member member, Class theClass)
        {
            Boolean result = false;
            if (theClass.Participants.Count() >= theClass.Capacity)
            {
                return result;
            }
            else
            {
                return result = await _classService.SignUpMemberToClass(member, theClass);
            }
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
