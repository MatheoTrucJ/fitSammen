using FitSammen_API.DatabaseAccessLayer;
using FitSammen_API.Model;
{
    
}

namespace FitsammenAPITest
{
    public class TestDBAccess
    {
        private readonly IClassAccess _classAccess = new ClassAccess("Server=localhost;Database=GeometryData;User Id=sa;Password=@12tf56so;TrustServerCertificate=True;");

        [Fact]
        public void MemberGetAllClassesSuccessNo()
        {
            //Arrange
            Class classToCompare = new Class(
                1,
                new DateOnly(2025, 11, 18),
                new Employee
                {
                    FirstName = "Anna",
                    LastName = "Trainer"
                },
                "Gentle flexibility and breathing session.",
                new Room
                {
                    RoomName = "Main Gym"
                },
                "Morning Yoga",
                30,
                60,
                new TimeOnly(9, 0),
                ClassType.Yoga);

            //Act
            IEnumerable<Class> retrievedClasses = _classAccess.GetUpcomingClasses();

            //Assert
            Assert.NotEmpty(retrievedClasses);
            Assert.Contains(retrievedClasses, c =>
                c.Id == classToCompare.Id &&
                c.TrainingDate == classToCompare.TrainingDate &&
                c.Instructor.FirstName == classToCompare.Instructor.FirstName &&
                c.Instructor.LastName == classToCompare.Instructor.LastName &&
                c.Description == classToCompare.Description &&
                c.Room.RoomName == classToCompare.Room.RoomName &&
                c.Name == classToCompare.Name &&
                c.Capacity == classToCompare.Capacity &&
                c.DurationInMinutes == classToCompare.DurationInMinutes &&
                c.StartTime == classToCompare.StartTime &&
                c.ClassType == classToCompare.ClassType);
        }
    }
}
