namespace FitSammenWebClient.Models
{
    public class Membership
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public MembershipStatus Status { get; set; }
        public Member Member { get; set; }
        public Payment? Payment { get; set; }

        public Membership(DateOnly startDate, DateOnly endDate, MembershipStatus status, Member member, Payment? payment)
        {
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            Member = member;
            Payment = payment;
        }
    }
    public enum MembershipStatus
    {
        basic,
        premium,
        paused,
        cancelled
    }
}
