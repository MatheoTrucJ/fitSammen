using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSammenDekstopClient.Model
{
    public class CreateClassRequest
    {
        public DateOnly TrainingDate { get; set; }
        public Employee? Instructor { get; set; }
        public string? Description { get; set; }
        public Room? Room { get; set; }
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public int DurationInMinutes { get; set; }
        public TimeOnly StartTime { get; set; }
        public ClassType ClassType { get; set; }
    }
}