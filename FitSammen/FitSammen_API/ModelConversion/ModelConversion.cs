using FitSammen_API.DTOs;
using FitSammen_API.Model;

namespace FitSammen_API.ModelConversion
{
    public class ModelConversion
    {
        public static ClassListItemDTO ToClassListItemDTO(Class cls)
        {
            var participantCount = cls.Participants?.Count() ?? 0;
            var remaining = cls.Capacity - participantCount;

            return new ClassListItemDTO
            {
                ClassId = cls.Id,

                Date = cls.TrainingDate.Date,
                IsAvailable = cls.TrainingDate.IsAvailable,

                ClassName = cls.Name,

                InstructorName = cls.Instructor != null
                    ? $"{cls.Instructor.FirstName} {cls.Instructor.LastName}"
                    : string.Empty,

                Description = cls.Description,
                ClassType = cls.ClassType,

                StartTime = cls.StartTime,
                DurationInMinutes = cls.DurationInMinutes,

                RoomName = cls.Room?.RoomName ?? string.Empty,

                Capacity = cls.Capacity,
                ParticipantCount = participantCount,
                RemainingSpots = remaining,
                IsFull = remaining <= 0
            };
        }
    }
}
