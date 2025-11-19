using System.ComponentModel.DataAnnotations;

namespace FitSammen_API.Model
{
    public abstract class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateOnly BirthDate { get; set; }
        public int User_ID { get; set; }
        public UserType UserType { get; set; }

        protected User(string firstName, string lastName, string email, string phone, DateOnly birthDate, int userID, UserType userType)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            BirthDate = birthDate;
            User_ID = userID;
            UserType = userType;
        }

        protected User()
        {
        }
    }
    public enum UserType
    {
        Administrator,
        Member,
        Employee
    }
}
