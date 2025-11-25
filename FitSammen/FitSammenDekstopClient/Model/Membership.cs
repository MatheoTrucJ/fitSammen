namespace FitSammenDekstopClient.Model
{
    public class Membership
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public MembershipType Status { get; set; }
        public Member Member { get; set; }
        public IEnumerable<Payment>? Payments { get; set; }

        public Membership(DateOnly startDate, DateOnly endDate, MembershipType status, Member member)
        {
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            Member = member;
        }
    }

    public record MembershipType(string Name, double Price) {
        public static readonly MembershipType Basic = new MembershipType("Basic", 199.99);
        public static readonly MembershipType Premium = new MembershipType("Premium", 299.99);
        public static readonly MembershipType Paused = new MembershipType("Paused", 0.0);
        public static readonly MembershipType Cancelled = new MembershipType("Cancelled", 0.0);
    }
}
