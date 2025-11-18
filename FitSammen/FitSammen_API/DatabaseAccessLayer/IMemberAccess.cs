using FitSammen_API.Model;

namespace FitSammen_API.DatabaseAccessLayer
{
    public interface IMemberAccess
    {
        public int CreateMemberBooking(int memberUserNumber, int classId);
        bool IsMemberBookingThereForTest(int memberBookingId);
    }
}
