using FitSammenWebClient.BusinessLogicLayer;
using FitSammenWebClient.Models;
using Microsoft.Extensions.Configuration;
namespace FitsammenMVCTests
{
    public class BusinessLogicLayerTest
    {
        [Fact]
        public void SignUpMemberWhenClassIsFull_ReturnIsFull()
        {
            // Arrange: fake IConfiguration
            var settings = new Dictionary<string, string?>
            {
                { "ServiceUrlToUse", "https://localhost:7033/api/" }
            };

            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            //Arrange
            Member m1 = new Member("Joe", "Hansen", "Fake1@email.dk", "12345678", DateOnly.FromDateTime(DateTime.Now), 1, UserType.Customer);
            Member m2 = new Member("John", "Johnny", "Fake2@email.dk", "87654321", DateOnly.FromDateTime(DateTime.Now), 2, UserType.Customer);
            Member testMember = new Member("TEST", "TEST", "TEST2@email.dk", "77777777", DateOnly.FromDateTime(DateTime.Now), 2, UserType.Customer);

            Employee instructor = new Employee("Kim", "Kimmer", "kim@12.dk", "23232323", DateOnly.FromDateTime(DateTime.Now), 3, UserType.Employee, "230802-2342");
            Location location = new Location("TestGade", 10, 9000, "Aalborg", "Danmark");
            Room room = new Room(1, "Room1", 2, location);
            Class fullClass = new Class(1, DateOnly.FromDateTime(DateTime.Now), instructor, "Fyldt klasse", room, "YOGA", 2, 120, TimeOnly.FromDateTime(DateTime.Now), ClassType.Yoga);

            fullClass.Participants = new List<MemberBooking>
            {
                new MemberBooking { Member = m1 },
                new MemberBooking { Member = m2 }
            };

            ClassLogic classLogic = new ClassLogic(config);

            //Act
            Boolean res = classLogic.signUpAMember(testMember, fullClass);

            //Assert
            Assert.False(res);
            Assert.Equal(2, fullClass.Participants.Count());
            Assert.DoesNotContain(fullClass.Participants, mb => mb.Member == testMember);
        }

        [Fact]
        public void SignUpMemberWhenClassIsNotFull_ReturnIsSuccess()
        {
            // Arrange: fake IConfiguration
            var settings = new Dictionary<string, string?>
            {
                { "ServiceUrlToUse", "https://localhost:7033/api/" } 
            };

            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            //Arrange
            Member m1 = new Member("Joe", "Hansen", "Fake1@email.dk", "12345678", DateOnly.FromDateTime(DateTime.Now), 1, UserType.Customer);
            Member m2 = new Member("John", "Johnny", "Fake2@email.dk", "87654321", DateOnly.FromDateTime(DateTime.Now), 2, UserType.Customer);
            Member testMember = new Member("TEST", "TEST", "TEST2@email.dk", "77777777", DateOnly.FromDateTime(DateTime.Now), 2, UserType.Customer);

            Employee instructor = new Employee("Kim", "Kimmer", "kim@12.dk", "23232323", DateOnly.FromDateTime(DateTime.Now), 3, UserType.Employee, "230802-2342");
            Location location = new Location("TestGade", 10, 9000, "Aalborg", "Danmark");
            Room room = new Room(1, "Room1", 2, location);
            Class notFullClass = new Class(1, DateOnly.FromDateTime(DateTime.Now), instructor, "Ikke Fyldt klasse", room, "YOGA", 3, 120, TimeOnly.FromDateTime(DateTime.Now), ClassType.Yoga);

            notFullClass.Participants = new List<MemberBooking>
            {
                new MemberBooking { Member = m1 },
                new MemberBooking { Member = m2 }
            };

            ClassLogic classLogic = new ClassLogic(config);

            //Act
            Boolean res = classLogic.signUpAMember(testMember, notFullClass);

            //Assert
            Assert.True(res);
            Assert.Equal(3, notFullClass.Participants.Count());
            Assert.Contains(notFullClass.Participants, mb => mb.Member == testMember);
        }
    }
}
