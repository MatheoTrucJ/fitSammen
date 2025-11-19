using FitSammen_API.BusinessLogicLayer;
using FitSammen_API.DTOs;
using FitSammen_API.Model;

namespace FitSammen_API.Mapping
{
    public class ModelConversion
    {
        public static ClassListItemDTO ToClassListItemDTO(Class cls)
        {
            Room room = new Room();
            room.Location = new Location();
            room.Location.Zipcode = new Zipcode();
            room.Location.Zipcode.City = new City();
            room.Location.StreetName = cls.Room.Location.StreetName;
            room.Location.StreetName += " " + cls.Room.Location.HouseNumber;
            room.Location.Zipcode.City.CityName = 
                cls.Room.Location.Zipcode.City.CityName;

            return new ClassListItemDTO
            {
                ClassId = cls.Id,
                TrainingDate = cls.TrainingDate,
                ClassName = cls.Name,
                ClassType = cls.ClassType,
                StartTime = cls.StartTime,
                DurationInMinutes = cls.DurationInMinutes,
                Capacity = cls.Capacity,
                MemberCount = cls.MemberCount,
                RemainingSpots = cls.Capacity - cls.MemberCount,
                Room = room
            };
        }

        public static BookingResponseDTO ToBookingResponseDTO(BookingResult result)
        {
            return new BookingResponseDTO
            {
                BookingId = result.BookingID ?? 0,
                Status = result.Status.ToString(),
                Message = result.Status switch
                {
                    BookingStatus.Success => "Booking successful.",
                    BookingStatus.ClassFull => "Booking failed: Member has already booked this class.",
                    BookingStatus.Error => "Booking failed: Unknown error.",
                    _ => "Booking failed: Unknown error."
                }
            };
        }
    }
}
