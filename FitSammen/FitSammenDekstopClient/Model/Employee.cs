namespace FitSammenDekstopClient.Model
{
    public class Employee : User
    {
        public string CPRNumber { get; set; }

        public string FullName => FirstName + " " + LastName; // Bruges I WinForms ComboBoxEmployee til at vise full name

        public Employee(string firstName, string lastName, string email, string phone, DateOnly birthDate, int userID, UserType userType, string CPRNumber) : 
        base(firstName, lastName, email, phone, birthDate, userID, userType)
        {
            this.CPRNumber = CPRNumber;
        }

        public Employee() : base()
        {
            this.CPRNumber = string.Empty;
        }

        
    }
}
