using FitSammen_API.DatabaseAccessLayer;
using FitSammen_API.Model;
{

}

namespace FitsammenAPITest
{
    public class TestDBAccess
    {
        private readonly IClassAccess _classAccess = new ClassAccess("Server=localhost;Database=FitSammenDB;User Id=sa;Password=@12tf56so;TrustServerCertificate=True;");
        private readonly IMemberAccess _memberAccess = new MemberAccess("Server=localhost;Database=FitSammenDB;User Id=sa;Password=@12tf56so;TrustServerCertificate=True;");

        [Fact]
        public void GetAllUpcomingClassesSuccessNo()
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
            Assert.Equal(retrievedClasses.First().Id, classToCompare.Id);
            Assert.Equal(retrievedClasses.First().TrainingDate, classToCompare.TrainingDate);
            Assert.Equal(retrievedClasses.First().Instructor.FirstName, classToCompare.Instructor.FirstName);
            Assert.Equal(retrievedClasses.First().Instructor.LastName, classToCompare.Instructor.LastName);
            Assert.Equal(retrievedClasses.First().Description, classToCompare.Description);
            Assert.Equal(retrievedClasses.First().Room.RoomName, classToCompare.Room.RoomName);
            Assert.Equal(retrievedClasses.First().Name, classToCompare.Name);
            Assert.Equal(retrievedClasses.First().Capacity, classToCompare.Capacity);
            Assert.Equal(retrievedClasses.First().DurationInMinutes, classToCompare.DurationInMinutes);
            Assert.Equal(retrievedClasses.First().StartTime, classToCompare.StartTime);
            Assert.Equal(retrievedClasses.First().ClassType, classToCompare.ClassType);
        }

        [Fact]
        public void CreateMemberBookingSuccess()
        {
            //Arrange

            //Act - create a member booking for member with user number 2 for class with id 1
            int memberBookingId = _memberAccess.CreateMemberBooking(2, 1);
            bool res = _memberAccess.IsMemberBookingThereForTest(memberBookingId);

            //Assert
            Assert.True(res);
        }
    }
}
