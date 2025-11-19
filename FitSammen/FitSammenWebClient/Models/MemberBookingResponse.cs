namespace FitSammenWebClient.Models
{
    public class MemberBookingResponse
    {
        public int BookingId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
