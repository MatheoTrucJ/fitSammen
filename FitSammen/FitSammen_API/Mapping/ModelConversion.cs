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
                    BookingStatus.ClassFull => "Booking failed: The class is already ful",
                    BookingStatus.Error => "Booking failed: Unknown error.",
                    BookingStatus.AlreadySignedUp => "Booking failed: Member is already signed up for this class",
                    _ => "Booking failed: Unknown error."
                }
            };
        }

        public static WaitingListEntryResponseDTO ToWaitingListEntryResponseDTO(WaitingListResult result)
        {
            WaitingListEntryResponseDTO dto = new WaitingListEntryResponseDTO();
            {
                dto.WaitingListPosition = result.WaitingListPosition ?? -1;
                dto.Status = result.Status;
                dto.Message = result.Status switch
                {
                    WaitingListStatus.Success => "Successfully added to the waiting list.",
                    WaitingListStatus.AlreadySignedUp => "Member is already on the waiting list.",
                    WaitingListStatus.Error => "Failed to add to the waiting list: Unknown error.",
                    _ => "Failed to add to the waiting list: Unknown error."
                };
            }
            return dto;
        }

        public static LocationDTO LocationToLocationDTO(Location location)
        {
            LocationDTO lDTO = new LocationDTO
            {
                LocationId = location.LocationId,
                StreetName = location.StreetName,
                HouseNumber = location.HouseNumber,
                ZipcodeNumber = location.Zipcode.ZipcodeNumber,
                CityName = location.Zipcode.City.CityName,
                CountryName = location.Zipcode.City.Country.CountryName
            };

            return lDTO;
        }

        public static RoomDTO RoomToDTO(Room room)
        {
            RoomDTO rDTO = new RoomDTO
            {
                RoomId = room.RoomId,
                RoomName = room.RoomName,
                Capacity = room.Capacity,
                LocationDTO = new LocationDTO
                {
                    LocationId = room.Location.LocationId,
                    StreetName = room.Location.StreetName,
                    HouseNumber = room.Location.HouseNumber,
                    ZipcodeNumber = room.Location.Zipcode.ZipcodeNumber,
                    CityName = room.Location.Zipcode.City.CityName,
                    CountryName = room.Location.Zipcode.City.Country.CountryName
                }
            };

            return rDTO;
        }
    }
}
