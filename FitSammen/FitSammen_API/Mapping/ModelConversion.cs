using FitSammen_API.BusinessLogicLayer;
using FitSammen_API.DTOs;
using FitSammen_API.Model;
using System.Net.NetworkInformation;

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

        public static Class ClassCreateRequestDTOToClass(ClassCreateRequestDTO cls)
        {
            Class res = new Class
            {
                TrainingDate = cls.TrainingDate,
                Instructor = EmployeeMinimalDTOToEmployee(cls.Instructor),
                Room = RoomMinimalDTOToRoom(cls.Room),
                Name = cls.Name,
                Description = cls.Description,
                Capacity = cls.Capacity,
                DurationInMinutes = cls.DurationInMinutes,
                StartTime = cls.StartTime,
                ClassType = cls.ClassType
            };
            return res;
        }

        public static ClassCreateResponseDTO ToClassCreateResponseDTO(int i)
        {
            ClassCreateResponseDTO dto;
            switch (i)
            {
                case 0:
                    dto = new ClassCreateResponseDTO(
                        ClassCreateStatus.Conflict,
                        "No class was created");
                    break; case 1:
                case -1:
                    dto = new ClassCreateResponseDTO(
                        ClassCreateStatus.Error,
                        "An error occurred while creating the class");
                    break;
                case -2:
                    dto = new ClassCreateResponseDTO(
                        ClassCreateStatus.BadRequest,
                        "Faulty parameters");
                    break;
                default:
                    dto = new ClassCreateResponseDTO(
                        ClassCreateStatus.Success,
                        "Class created successfully");
                    break;
            }
            return dto;
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

        public static LocationListDTO LocationToLocationListDTO(Location location)
        {
            LocationListDTO lDTO = new LocationListDTO(
                location.LocationId,
                location.StreetName,
                location.HouseNumber,
                location.Zipcode.ZipcodeNumber,
                location.Zipcode.City.CityName
                );


            return lDTO;
        }

        public static RoomListDTO RoomToRoomListDTO(Room room)
        {
            RoomListDTO rlDTO = new RoomListDTO(
                room.RoomId,
                room.RoomName,
                room.Capacity,
                new LocationMinimalDTO(room.Location.LocationId)
                );
            return rlDTO;
        }

        public static Room RoomMinimalDTOToRoom(RoomMinimalDTO room)
        {
            Room r = new Room
            {
                RoomId = room.RoomId
            };
            return r;
        }

        public static EmployeeListDTO EmployeeToEmployeeListDTO(Employee employee)
        {
            EmployeeListDTO employeeListDTO = new EmployeeListDTO(
                employee.User_ID,
                employee.FirstName,
                employee.LastName
            );
            return employeeListDTO;
        }

        public static Employee EmployeeMinimalDTOToEmployee(EmployeeMinimalDTO employeeDTO)
        {
            Employee employee = new Employee
            {
                User_ID = employeeDTO.EmployeeId
            };
            return employee;
        }
    }
}
