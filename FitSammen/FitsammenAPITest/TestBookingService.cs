using FitSammen_API.BusinessLogicLayer;
using FitSammen_API.DatabaseAccessLayer;
using FitSammen_API.Exceptions;
using FitSammen_API.Model;
using System;
using Xunit;

namespace FitSammen_APITest
{
    public class BookingServiceTests
    {
        [Fact]
        public void BookClass_ReturnsAlreadySignedUp_WhenMemberAlreadyBooked()
        {
            // Arrange
            var fakeMemberAccess = new MemberAccessMock { AlreadySignedUp = true };
            var service = new BookingService(fakeMemberAccess);

            // Act
            var result = service.BookClass(1, 10);

            // Assert
            Assert.Equal(BookingStatus.AlreadySignedUp, result.Status);
            Assert.Null(result.BookingID);
        }

        [Fact]
        public void BookClass_ReturnsSuccess_WhenDalCreatesBooking()
        {
            // Arrange
            var fakeMemberAccess = new MemberAccessMock { NewBookingId = 42 };
            var service = new BookingService(fakeMemberAccess);

            // Act
            var result = service.BookClass(1, 10);

            // Assert
            Assert.Equal(BookingStatus.Success, result.Status);
            Assert.Equal(42, result.BookingID);
        }

        [Fact]
        public void BookClass_ReturnsError_WhenDalThrowsDataAccessException()
        {
            // DataAccessException means the DAL call failed before returning a booking id
            // Arrange
            var fakeMemberAccess = new MemberAccessMock { ThrowDataAccessException = true };
            var service = new BookingService(fakeMemberAccess);

            // Act
            var result = service.BookClass(1, 10);

            // Assert
            Assert.Equal(BookingStatus.Error, result.Status);
            Assert.Null(result.BookingID);
        }

        [Fact]
        public void BookClass_ReturnsClassFull_WhenDalReturnsZeroBookingId()
        {
            // Arrange
            var fakeMemberAccess = new MemberAccessMock { NewBookingId = 0 };
            var service = new BookingService(fakeMemberAccess);

            // Act
            var result = service.BookClass(1, 10);

            // Assert
            Assert.Equal(BookingStatus.ClassFull, result.Status);
            Assert.Null(result.BookingID);
        }

        [Fact]
        public void BookClass_ReturnsError_WhenDalReturnsNegativeBookingId()
        {
            // Arrange
            var fakeMemberAccess = new MemberAccessMock { NewBookingId = -1 };
            var service = new BookingService(fakeMemberAccess);

            // Act
            var result = service.BookClass(1, 10);

            // Assert
            Assert.Equal(BookingStatus.Error, result.Status);
            Assert.Null(result.BookingID);
        }

        private sealed class MemberAccessMock : IMemberAccess
        {
            public int NewBookingId { get; set; }
            public bool ThrowDataAccessException { get; set; }
            public bool ThrowGenericException { get; set; }
            public bool AlreadySignedUp { get; set; }

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

            public int CreateWaitingListEntry(int memberUserId, int classId)
            {
                throw new NotImplementedException();
            }

            public User FindUserByEmailAndPassword(string email, byte[] password)
            {
                throw new NotImplementedException();
            }

            public int GetMemberCountFromClassId(int classId)
            {
                throw new NotImplementedException();
            }

            public byte[] GetSaltByEmail(string email)
            {
                throw new NotImplementedException();
            }

            public int IsMemberOnWaitingList(int memberUserId, int classId)
            {
                throw new NotImplementedException();
            }

            public bool IsMemberSignedUp(int memberUserNumber, int classID) => AlreadySignedUp;
        }

        
    }
}
