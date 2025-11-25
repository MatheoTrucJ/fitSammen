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

        public static LocationDTO LocationToLocationDTO(Location locations)
        {
            LocationDTO lDTO = new LocationDTO
            {
                LocationId = locations.LocationId,
                StreetName = locations.StreetName,
                HouseNumber = locations.HouseNumber,
                ZipcodeNumber = locations.Zipcode.ZipcodeNumber,
                CityName = locations.Zipcode.City.CityName,
                CountryName = locations.Zipcode.City.Country.CountryName
            };

            return lDTO;
        }

        public static RoomDTO RoomToDTO(Room rooms)
        {
            RoomDTO rDTO = new RoomDTO
            {
                RoomId = rooms.RoomId,
                RoomName = rooms.RoomName,
                Capacity = rooms.Capacity,
                LocationDTO = new LocationDTO
                {
                    LocationId = rooms.Location.LocationId,
                    StreetName = rooms.Location.StreetName,
                    HouseNumber = rooms.Location.HouseNumber,
                    ZipcodeNumber = rooms.Location.Zipcode.ZipcodeNumber,
                    CityName = rooms.Location.Zipcode.City.CityName,
                    CountryName = rooms.Location.Zipcode.City.Country.CountryName
                }
            };

            return rDTO;
        }
    }
}
