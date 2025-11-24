namespace FitSammen_API.Model
{
    public class WaitingListEntry
    {
        public int WaitingListId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Member Member { get; set; }
        public Class Class { get; set; }

        public WaitingListEntry(int waitingListId, DateTime createdAt, Member member, Class @class)
        {
            WaitingListId = waitingListId;
            CreatedAt = this.CreatedAt;
            Member = member;
            Class = @class;
        }
    }
}
