namespace FitSammen_API.Model
{
    public class Class
    {
        public int Id { get; set; }
        public TrainingDate TrainingDate { get; set; }
        public Employee Instructor { get; set; }
        public Room Room { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<MemberBooking> Participants { get; set; }
        public IEnumerable<WaitingListEntry> WaitingListEntries { get; set; }
        public int Capacity { get; set; }
        public int DurationInMinutes { get; set; }
        public TimeOnly StartTime { get; set; }
        public ClassType ClassType { get; set; }

        public Class(int id, TrainingDate trainingDate, Employee instructor, 
            Room room, string name, int capacity, int durationInMinutes, TimeOnly startTime, ClassType classType)
        {
            Id = id;
            TrainingDate = trainingDate;
            Instructor = instructor;
            Room = room;
            Name = name;
            Capacity = capacity;
            DurationInMinutes = durationInMinutes;
            StartTime = startTime;
            ClassType = classType;
            Participants = new List<MemberBooking>();
        }

        public void addMember(MemberBooking booking)
        {
           Participants = Participants.Append(booking);
        }

    }
    public enum ClassType
    {
        Yoga,
        Spinning,
        StrengthTraining,
        Cardio,
        Other
    }
}
