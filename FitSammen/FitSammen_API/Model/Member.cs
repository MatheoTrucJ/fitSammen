
namespace FitSammen_API.Model
{
    public class Member : User
    {
        public IEnumerable<Membership> Memberships { get; set; }
        public Member(string firstName, string lastName, string email, string phone, DateOnly birthDate, int userNumber, UserType userType) : 
        base(firstName, lastName, email, phone, birthDate, userNumber, userType)
        {
        }
    }
}
