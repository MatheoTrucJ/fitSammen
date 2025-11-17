namespace FitSammenWebClient.Models
{
    public class MemberBooking
    {
        public int MemberBookingId { get; set; }
        public Member Member { get; set; }
        public Class Class { get; set; }
    }
}
