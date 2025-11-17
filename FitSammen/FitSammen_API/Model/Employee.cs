namespace FitSammen_API.Model
{
    public class Employee : User
    {
        public string CPRNumber { get; set; }
        public Employee(string firstName, string lastName, string email, string phone, DateOnly birthDate, int userNumber, UserType userType, string CPRNumber) : 
        base(firstName, lastName, email, phone, birthDate, userNumber, userType)
        {
            this.CPRNumber = CPRNumber;
        }

        public Employee() : base()
        {
            this.CPRNumber = string.Empty;
        }
    }
}
