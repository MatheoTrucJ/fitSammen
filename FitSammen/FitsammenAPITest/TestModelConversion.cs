using System;
using FitSammen_API.Mapping;
using FitSammen_API.Model;
using FitSammen_API.BusinessLogicLayer;
using Xunit;
using FitSammen_API.DTOs;

namespace FitsammenAPITest
{
    public class TestModelConversion
    {
        [Fact]
        public void ToClassListItemDTO_MapsCoreFields()
        {
            // arrange
            var location = new Location(1,"Main St", 10, 12345, "Cityville", "Countryland");
            var room = new Room(1, "Room A", 30, location);
            var instructor = new Employee();
            var cls = new Class(
                id: 5,
                trainingDate: new DateOnly(2025, 1, 1),
                instructor: instructor,
                description: "Functional strength training",
                room: room,
                name: "Strength",
                capacity: 25,
                memberCount: 0,
                durationInMinutes: 50,
                startTime: new TimeOnly(18, 30),
                classType: ClassType.StrengthTraining
            );

            // Add participants via bookings
            var member1 = new Member();
            var member2 = new Member();
            cls.addMember(new MemberBooking(1, member1, cls));
            cls.addMember(new MemberBooking(2, member2, cls));

            // act
            var dto = ModelConversion.ToClassListItemDTO(cls);

            // assert
            Assert.Equal(5, dto.ClassId);
            Assert.Equal(new DateOnly(2025, 1, 1), dto.TrainingDate);
            Assert.Equal("Strength", dto.ClassName);
            Assert.Equal(ClassType.StrengthTraining, dto.ClassType);
            Assert.Equal(new TimeOnly(18, 30), dto.StartTime);
            Assert.Equal(50, dto.DurationInMinutes);
            Assert.Equal(25, dto.Capacity);
            Assert.Equal(2, dto.MemberCount); // uses Participants.Count()
            Assert.NotNull(dto.Room);
            Assert.Equal("Main St 10", dto.Room.Location.StreetName);
            Assert.Equal("Cityville", dto.Room.Location.Zipcode.City.CityName);
        }

        [Fact]
        public void ToBookingResponseDTO_MapsSuccess()
        {
            var result = new BookingResult { Status = BookingStatus.Success, BookingID = 10 };
            var dto = ModelConversion.ToBookingResponseDTO(result);
            Assert.Equal(10, dto.BookingId);
            Assert.Equal("Success", dto.Status);
            Assert.Equal("Booking successful.", dto.Message);
        }

        [Fact]
        public void ToBookingResponseDTO_ClassFull_NullIdToZero()
        {
            var result = new BookingResult { Status = BookingStatus.ClassFull, BookingID = null };
            var dto = ModelConversion.ToBookingResponseDTO(result);
            Assert.Equal(0, dto.BookingId);
            Assert.Equal("ClassFull", dto.Status);
            Assert.Equal("Booking failed: Member has already booked this class.", dto.Message);
        }

        [Fact]
        public void ToBookingResponseDTO_Error_DefaultMessage()
        {
            var result = new BookingResult { Status = BookingStatus.Error, BookingID = null };
            var dto = ModelConversion.ToBookingResponseDTO(result);
            Assert.Equal(0, dto.BookingId);
            Assert.Equal("Error", dto.Status);
            Assert.Equal("Booking failed: Unknown error.", dto.Message);
        }

        [Fact]
        public void ToBookingResponseDTO_UnknownStatus_DefaultMessage()
        {
            var result = new BookingResult { Status = (BookingStatus)999, BookingID = null };
            var dto = ModelConversion.ToBookingResponseDTO(result);
            Assert.Equal(0, dto.BookingId);
            Assert.Equal("999", dto.Status);
            Assert.Equal("Booking failed: Unknown error.", dto.Message);
        }

        [Fact]
        public void ToWaitingListResponseDTOSuccess()
        {
            WaitingListResult result = new WaitingListResult
            {
                Status = WaitingListStatus.Success,
                WaitingListPosition = 3
            };

            WaitingListEntryResponseDTO dto = ModelConversion.ToWaitingListEntryResponseDTO(result);

            Assert.Equal(3, dto.WaitingListPosition);
            Assert.Equal(WaitingListStatus.Success, dto.Status);
        }

        [Fact]
        public void ToWaitingListResponseDTOFull()
        {
            WaitingListResult result = new WaitingListResult
            {
                Status = WaitingListStatus.AlreadySignedUp,
                WaitingListPosition = 2
            };

            WaitingListEntryResponseDTO dto = ModelConversion.ToWaitingListEntryResponseDTO(result);

            Assert.Equal(2, dto.WaitingListPosition);
            Assert.Equal(WaitingListStatus.AlreadySignedUp, dto.Status);
        }

        [Fact]
        public void ToWaitingListResponseDTOError()
        {
            WaitingListResult result = new WaitingListResult
            {
                Status = WaitingListStatus.Error,
                WaitingListPosition = null
            };
            WaitingListEntryResponseDTO dto = ModelConversion.ToWaitingListEntryResponseDTO(result);

            Assert.Equal(-1, dto.WaitingListPosition);
            Assert.Equal(WaitingListStatus.Error, dto.Status);
        }
    }
}