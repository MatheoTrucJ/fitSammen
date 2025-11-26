namespace FitSammen_API.Model
{
    public class Class
    {
        public int Id { get; set; }
        public DateOnly TrainingDate { get; set; }
        public Employee Instructor { get; set; }
        public Room Room { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<MemberBooking> Participants { get; set; }
        public IEnumerable<WaitingListEntry> WaitingListEntries { get; set; }
        public int Capacity { get; set; }
        public int MemberCount { get; set; }
        public int DurationInMinutes { get; set; }
        public TimeOnly StartTime { get; set; }
        public ClassType ClassType { get; set; }

        public Class(int id, DateOnly trainingDate, Employee instructor, string description,
            Room room, string name, int capacity, int memberCount, int durationInMinutes, TimeOnly startTime, ClassType classType)
        {
            Id = id;
            TrainingDate = trainingDate;
            Instructor = instructor;
            Description = description;
            Room = room;
            Name = name;
            Capacity = capacity;
            MemberCount = memberCount;
            DurationInMinutes = durationInMinutes;
            StartTime = startTime;
            ClassType = classType;
            Participants = new List<MemberBooking>();
        }

        public Class()
        {
        }

        public void addMember(MemberBooking booking)
        {
           Participants = Participants.Append(booking);
        }

    }
    public enum ClassType
    {
        Yoga = 1,
        StrengthTraining = 2,
        Spinning = 3,
        Cardio = 4,
        Other = 5
    }
}
