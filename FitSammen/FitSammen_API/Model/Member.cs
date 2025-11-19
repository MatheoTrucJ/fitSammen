
namespace FitSammen_API.Model
{
    public class Member : User
    {
        public IEnumerable<Membership> Memberships { get; set; }
        public Member(string firstName, string lastName, string email, string phone, DateOnly birthDate, int userID, UserType userType) : 
        base(firstName, lastName, email, phone, birthDate, userID, userType)
        {
        }
        public Member() : base()
        {
            this.Memberships = Array.Empty<Membership>();
        }
    }
}
