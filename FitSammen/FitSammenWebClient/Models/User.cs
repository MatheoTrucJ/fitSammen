using System.ComponentModel.DataAnnotations;

namespace FitSammenWebClient.Models
{
    public abstract class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateOnly BirthDate { get; set; }
        public int User_Id { get; set; }
        public UserType UserType { get; set; }

        protected User(string firstName, string lastName, string email, string phone, DateOnly birthDate, int userNumber, UserType userType)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            BirthDate = birthDate;
            User_Id = userNumber;
            UserType = userType;
        }

        protected User()
        {
            
        }
    }
    public enum UserType
    {
        Administrator,
        Customer,
        Employee
    }

}
