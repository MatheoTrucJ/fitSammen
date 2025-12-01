using FitSammen_API.Model;

namespace FitSammen_API.DatabaseAccessLayer
{
    public interface IMemberAccess
    {
        public int CreateMemberBooking(int memberUserNumber, int classId);
        bool IsMemberSignedUp(int memberNumber, int classID);

        public int CreateWaitingListEntry(int memberUserId, int classId);

        public int IsMemberOnWaitingList(int memberUserId, int classId);
        User FindUserByEmailAndPassword(string email, string password);
    }
}
