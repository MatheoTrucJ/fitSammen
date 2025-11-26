using FitSammen_API.Model;
using System.ComponentModel.DataAnnotations;

namespace FitSammen_API.DTOs
{
    public class ClassCreateRequestDTO
    {
        public DateOnly TrainingDate { get; set; }
        public EmployeeMinimalDTO Instructor { get; set; }
        public string Description { get; set; }
        public RoomMinimalDTO Room { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 100)]
        public int Capacity { get; set; }
        [Range(1, 120)]
        public int DurationInMinutes { get; set; }
        public TimeOnly StartTime { get; set; }
        public ClassType ClassType { get; set; }

        public ClassCreateRequestDTO() { }

        public ClassCreateRequestDTO(DateOnly trainingDate, EmployeeMinimalDTO instructor, string description, RoomMinimalDTO room, string name, int capacity, int durationInMinutes, TimeOnly startTime, ClassType classType)
        {
            TrainingDate = trainingDate;
            Instructor = instructor;
            Description = description;
            Room = room;
            Name = name;
            Capacity = capacity;
            DurationInMinutes = durationInMinutes;
            StartTime = startTime;
            ClassType = classType;
        }
    }
}
