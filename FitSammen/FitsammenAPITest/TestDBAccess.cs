using FitSammen_API.DatabaseAccessLayer;
using FitSammen_API.Exceptions;
using FitSammen_API.Model;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Transactions;
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
                    RoomName = "Main Gym",
                    Location = new Location
                    {
                        StreetName = "Fitness Street",
                        HouseNumber = 12,
                        Zipcode = new Zipcode
                        {
                            City = new City
                            {
                                CityName = "Copenhagen"
                            }
                        }
                    }
                },
                "Morning Yoga",
                30,
                0,
                60,
                new TimeOnly(9, 0),
                ClassType.Yoga);

            //Act
            IEnumerable<Class> retrievedClasses = _classAccess.GetUpcomingClasses();

            //Assert
            Assert.NotEmpty(retrievedClasses);
            Assert.Equal(retrievedClasses.FirstOrDefault().Id, classToCompare.Id);
            Assert.Equal(retrievedClasses.FirstOrDefault().TrainingDate, classToCompare.TrainingDate);
            Assert.Equal(retrievedClasses.FirstOrDefault().Instructor.FirstName, classToCompare.Instructor.FirstName);
            Assert.Equal(retrievedClasses.FirstOrDefault().Instructor.LastName, classToCompare.Instructor.LastName);
            Assert.Equal(retrievedClasses.FirstOrDefault().Description, classToCompare.Description);
            Assert.Equal(retrievedClasses.FirstOrDefault().Room.RoomName, classToCompare.Room.RoomName);
            Assert.Equal(retrievedClasses.FirstOrDefault().Name, classToCompare.Name);
            Assert.Equal(retrievedClasses.FirstOrDefault().Capacity, classToCompare.Capacity);
            Assert.Equal(retrievedClasses.FirstOrDefault().DurationInMinutes, classToCompare.DurationInMinutes);
            Assert.Equal(retrievedClasses.FirstOrDefault().StartTime, classToCompare.StartTime);
            Assert.Equal(retrievedClasses.FirstOrDefault().ClassType, classToCompare.ClassType);
            Assert.Equal(retrievedClasses.FirstOrDefault().Room.Location.StreetName, classToCompare.Room.Location.StreetName);
            Assert.Equal(retrievedClasses.FirstOrDefault().Room.Location.HouseNumber, classToCompare.Room.Location.HouseNumber);
            Assert.Equal(retrievedClasses.FirstOrDefault().Room.Location.Zipcode.City.CityName, classToCompare.Room.Location.Zipcode.City.CityName);
        }

        [Fact]
        public void CreateMemberBookingSuccess()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //Arrange

                //Act - create a member booking for member with user number 2 for class with id 1
                int memberBookingId = _memberAccess.CreateMemberBooking(2, 1);
                bool res = _memberAccess.IsMemberSignedUp(2, 1);

                //Assert
                Assert.True(res);

                scope.Dispose();
            }
        }

        [Fact]
        public void CreateMemberBookingClassFullFail()
        {
            //Arrange
            //Act
            int res = _memberAccess.CreateMemberBooking(3, 2);
            //Arrange
            Assert.Equal(0, res);
        }

        [Fact]
        public void WhenMakingAWaitingListEntryOnAFullClass_ThenMyPositionIsCorrect()
        {
                //Arrange
                //Act
                int SuccesPosition = _memberAccess.CreateWaitingListEntry(2, 5);
                //Assert
                Assert.Equal(1, SuccesPosition);
        }

        [Fact]
        public void WhenMakingAWaitingListEntryOnNotFullClass_ThenIShouldNotGetPositivePosition()
        {
                //Arrange
                //Act
                int FailedAndShowedFalsePosition = _memberAccess.CreateWaitingListEntry(2, 1);
                //Assert
                Assert.Equal(-1, FailedAndShowedFalsePosition);
        }

        [Fact]
        public void WhenMakingAWaitingListTwice_ThenMyPositionIsShownAndImNotAddedAgain()
        {
                //Arrange
                //Act
                int FailedAndShowedPosition = _memberAccess.IsMemberOnWaitingList(2, 4);

                //Assert
                Assert.Equal(1, FailedAndShowedPosition);
        }

        [Fact]
        public void WhenGettingAllLocations_AllTheLocationsAreReturned()
        {
            //Arrange
            City CityToCompare = new City
            {
                CityName = "Copenhagen"
            };
            Zipcode ZipcodeToCompare = new Zipcode
            {
                ZipcodeNumber = 2100,
                City = CityToCompare
            };
            Location LocationToCompare = new Location
            {
                LocationId = 1,
                StreetName = "Fitness Street",
                HouseNumber = 12,
                Zipcode = ZipcodeToCompare
            };
            //Act
            IEnumerable<Location> retrievedLocations = _classAccess.GetAllLocations();
            //Assert
            Assert.Equal(retrievedLocations.FirstOrDefault().LocationId, LocationToCompare.LocationId);
            Assert.Equal(retrievedLocations.FirstOrDefault().StreetName, LocationToCompare.StreetName);
            Assert.Equal(retrievedLocations.FirstOrDefault().HouseNumber, LocationToCompare.HouseNumber);
            Assert.Equal(retrievedLocations.FirstOrDefault().Zipcode.ZipcodeNumber, LocationToCompare.Zipcode.ZipcodeNumber);
            Assert.Equal(retrievedLocations.FirstOrDefault().Zipcode.City.CityName, LocationToCompare.Zipcode.City.CityName);
        }

        [Fact]
        public void WhenChoosingALocation_AllTheRoomsAreReturned()
        {
            //Arrange
            Location LocationToCompare = new Location
            {
                LocationId = 1
            };
            Room RoomToCompare = new Room
            {
                RoomId = 1,
                RoomName = "Main Gym",
                Capacity = 30,
                Location = LocationToCompare
            };
            //Act
            IEnumerable<Room> RetrievedRooms = _classAccess.GetRoomsByLocationId(1);
            //Assert
            Assert.Equal(RetrievedRooms.FirstOrDefault().RoomId, RoomToCompare.RoomId);
            Assert.Equal(RetrievedRooms.FirstOrDefault().RoomName, RoomToCompare.RoomName);
            Assert.Equal(RetrievedRooms.FirstOrDefault().Capacity, RoomToCompare.Capacity);
            Assert.Equal(RetrievedRooms.FirstOrDefault().Location.LocationId, RoomToCompare.Location.LocationId);
        }
    }
}
