namespace FitSammen_API.Model
{
    public class MemberBooking
    {
        public int MemberBookingId { get; set; }
        public Member Member { get; set; }
        public ClassCreateRequestDTO Class { get; set; }

        public MemberBooking(int memberBookingId, Member member, ClassCreateRequestDTO @class)
        {
            MemberBookingId = memberBookingId;
            Member = member;
            Class = this.Class;
        }
    }
}
