using Newtonsoft.Json;

namespace FitSammenWebClient.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        public DateOnly TrainingDate { get; set; }
        public Employee Instructor { get; set; }
        public string Description { get; set; }
        public Room Room { get; set; }
        public String Name { get; set; }
        public IEnumerable<MemberBooking> Participants { get; set; }
        public int MemberCount { get; set; }
        public IEnumerable<WaitingListEntry> WaitingListEntries { get; set; }
        public int Capacity { get; set; }
        public int DurationInMinutes { get; set; }
        public TimeOnly StartTime { get; set; }
        public ClassType ClassType { get; set; }

        public int RemainingSpots { get; set; }


        public Class(int id, DateOnly trainingDate, Employee employee, string description,
            Room room, string name, int capacity, int durationInMinutes, TimeOnly startTime, ClassType classType)
        {
            ClassId = id;
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
            RemainingSpots = Capacity - MemberCount;
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
