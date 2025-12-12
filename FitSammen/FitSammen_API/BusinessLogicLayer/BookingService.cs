using FitSammen_API.DatabaseAccessLayer;
using FitSammen_API.Exceptions;
using System;

namespace FitSammen_API.BusinessLogicLayer
{
    public class BookingService : IBookingService
    {
        private readonly IMemberAccess _memberAccess;

        public BookingService(IMemberAccess memberAccess)
        {
            _memberAccess = memberAccess;
        }

        public BookingResult BookClass(int memberId, int classId)
        {
            if (_memberAccess.IsMemberSignedUp(memberId, classId))
            {
                return new BookingResult
                {
                    Status = BookingStatus.AlreadySignedUp,
                    BookingID = null
                };
            }

            try
            {
                int bookingId = _memberAccess.CreateMemberBooking(memberId, classId);

                if (bookingId > 0)
                {
                    return new BookingResult
                    {
                        Status = BookingStatus.Success,
                        BookingID = bookingId
                    };
                }
                else if (bookingId == 0)
                {
                    return new BookingResult
                    {
                        Status = BookingStatus.ClassFull,
                        BookingID = null
                    };
                }
                else
                {
                    return new BookingResult
                    {
                        Status = BookingStatus.Error,
                        BookingID = null
                    };
                }
            }
            catch (DataAccessException)
            {
                return new BookingResult
                {
                    Status = BookingStatus.Error,
                    BookingID = null
                };
            }
        }
    }
}
