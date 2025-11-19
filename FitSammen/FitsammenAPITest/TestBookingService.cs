using FitSammen_API.BusinessLogicLayer;
using FitSammen_API.DatabaseAccessLayer;
using FitSammen_API.Exceptions;
using System;
using Xunit;

namespace FitSammen_APITest
{
    public class BookingServiceTests
    {
        [Fact]
        public void BookClass_ReturnsSuccess_WhenDalCreatesBooking()
        {
            // Arrange
            var fakeMemberAccess = new MemberAccessMock
            {
                NewBookingId = 42
            };

            var service = new BookingService(fakeMemberAccess);

            int memberId = 1;
            int classId = 10;

            // Act
            var result = service.BookClass(memberId, classId);

            // Assert
            Assert.Equal(BookingStatus.Success, result.Status);
            Assert.Equal(42, result.BookingID);
        }

        // Class full: DAL throws DataAccessException
        [Fact]
        public void BookClass_ReturnsClassFull_WhenDalThrowsDataAccessException()
        {
            // Arrange
            var fakeMemberAccess = new MemberAccessMock
            {
                ThrowDataAccessException = true
            };

            var service = new BookingService(fakeMemberAccess);

            int memberId = 1;
            int classId = 10;

            // Act
            var result = service.BookClass(memberId, classId);

            // Assert
            Assert.Equal(BookingStatus.ClassFull, result.Status);
            Assert.Equal(0, result.BookingID);
        }

        // Other errors: DAL throws generic Exception
        [Fact]
        public void BookClass_ReturnsError_WhenDalThrowsGenericException()
        {
            // Arrange
            var fakeMemberAccess = new MemberAccessMock
            {
                ThrowGenericException = true
            };

            var service = new BookingService(fakeMemberAccess);

            int memberId = 1;
            int classId = 10;

            // Act
            var result = service.BookClass(memberId, classId);

            // Assert
            Assert.Equal(BookingStatus.Error, result.Status);
            Assert.Equal(0, result.BookingID);
        }

        private sealed class MemberAccessMock : IMemberAccess
        {
            public int NewBookingId { get; set; }
            public bool ThrowDataAccessException { get; set; }
            public bool ThrowGenericException { get; set; }

            public int CreateMemberBooking(int memberUserNumber, int classId)
            {
                if (ThrowGenericException)
                {
                    throw new Exception("Unexpected DAL error");
                }

                if (ThrowDataAccessException)
                {
                    throw new DataAccessException("Could not create member booking - class may be full");
                }

                return NewBookingId;
            }

            public bool IsMemberSignedUp(int memberBookingId)
            {
                throw new NotImplementedException();
            }
        }
    }
}
