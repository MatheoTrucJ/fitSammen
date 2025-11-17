namespace FitSammenWebClient.Models
{
    public class Class
    {
        public int Id { get; set; }
        public TrainingDate TrainingDate { get; set; }
        public Employee Instructor { get; set; }
        public string Description { get; set; }
        public Room Room { get; set; }
        public String Name { get; set; }
        public IEnumerable<MemberBooking> Participants { get; set; }
        public WaitingList WaitingList { get; set; }
        public int Capacity { get; set; }
        public int DurationInMinutes { get; set; }
        public TimeOnly StartTime { get; set; }
        public ClassType ClassType { get; set; }

        public Class(int id, TrainingDate trainingDate, Employee employee, string description,
            Room room, string name, int capacity, int durationInMinutes, TimeOnly startTime, ClassType classType)
        {
            Id = id;
            TrainingDate = trainingDate;
            Description = description;
            Instructor = employee;
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
