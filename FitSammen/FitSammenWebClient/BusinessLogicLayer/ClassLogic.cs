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

        public Boolean signUpAMember(Member member, Class theClass)
        {
            if (theClass.Participants.Count() >= theClass.Capacity)
            {
                return false;
            }
            else
            {
                MemberBooking newBooking = new MemberBooking
                {
                    Member = member,
                    Class = theClass,
                };
                theClass.addMember(newBooking);
                return true;
            }
        }

        public async Task<IEnumerable<Class>> GetAllClassesAsync()
        {
            IEnumerable<Class>? allClasses = null;

            try
            {
                allClasses = await _classService.GetClasses();

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
