using FitSammenWebClient.Models;

namespace FitSammenWebClient.BusinessLogicLayer
{
    public class ClassLogic
    {
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
    }
}
