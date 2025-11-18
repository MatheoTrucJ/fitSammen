namespace FitSammen_API.Model
{
    public class MemberBooking
    {
        public int MemberBookingId { get; set; }
        public Member Member { get; set; }
        public Class Class { get; set; }

        public MemberBooking(int memberBookingId, Member member, Class @class)
        {
            MemberBookingId = memberBookingId;
            Member = member;
            Class = this.Class;
        }
    }
}
