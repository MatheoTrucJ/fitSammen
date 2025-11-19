using FitSammen_API.Model;
using System.Collections.Generic;

namespace FitSammen_API.DTOs
{
    public class ClassListItemDTO
    {
        public int ClassId { get; set; }
        public int DurationInMinutes { get; set; }
        public int Capacity { get; set; }
        public int MemberCount { get; set; }
        public int RemainingSpots { get; set; } 
        public string ClassName { get; set; } = string.Empty;
        public Room Room { get; set; }
        public ClassType ClassType { get; set; }
        public DateOnly TrainingDate { get; set; }
        public TimeOnly StartTime { get; set; }
    }
}
