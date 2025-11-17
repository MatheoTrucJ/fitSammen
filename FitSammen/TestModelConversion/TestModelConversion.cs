using System.Collections.Generic;
using FitSammen_API.Model;
using FitSammen_API.ModelConversion;
using Xunit;

namespace TestModelConversion   
{
    public class TestModelConversion
    {
        [Fact]
        public void ToClassListItemDto_MapsDomainClassToDtoCorrectly()
        {
            // arrange
            var trainingDate = new TrainingDate(
                DateOnly.Parse("2025-01-01"),
                true,
                "Morning Strength"
            );

            var instructor = new Employee(
                firstName: "John",
                lastName: "Doe",
                email: "john.doe@example.com",
                phone: "12345678",
                birthDate: new DateOnly(1990, 1, 1),
                userNumber: 1001,
                userType: UserType.Employee,
                CPRNumber: 123456789
            );

            var location = new Location(
                streetName: "Main St",
                housenumber: 10,
                zipCodeNumber: 12345,
                cityName: "Cityville",
                countryName: "Countryland"
            );

            var room = new Room(
                roomId: 1,
                roomName: "Room A",
                capacity: 20,
                location: location
            );

            var cls = new Class(
                id: 1,
                trainingDate: trainingDate,
                instructor: instructor,
                description: "Best morning workout ever",
                room: room,
                name: "Morning Strength",
                capacity: 10,
                durationInMinutes: 60,
                startTime: new TimeOnly(9, 0),
                classType: ClassType.StrengthTraining
            );
            
            cls.addMember(new MemberBooking());
            cls.addMember(new MemberBooking());
            cls.addMember(new MemberBooking());

            // act
            var dto = ModelConversion.ToClassListItemDTO(cls);

            // assert
            Assert.Equal(1, dto.ClassId);
            Assert.Equal(new DateOnly(2025, 1, 1), dto.Date);
            Assert.True(dto.IsAvailable);
            Assert.Equal("Morning Strength", dto.ClassName);
            Assert.Equal("John Doe", dto.InstructorName);
            Assert.Equal("Best morning workout ever", dto.Description);
            Assert.Equal(ClassType.StrengthTraining, dto.ClassType);
            Assert.Equal(new TimeOnly(9, 0), dto.StartTime);
            Assert.Equal(60, dto.DurationInMinutes);
            Assert.Equal("Room A", dto.RoomName);
            Assert.Equal(10, dto.Capacity);
            Assert.Equal(3, dto.ParticipantCount);
            Assert.Equal(7, dto.RemainingSpots);
            Assert.False(dto.IsFull);
        }
    }
}