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
                Name = cls.Name,
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

        public static ClassCreateResponseDTO ToClassCreateResponseDTO(BookingClassResult bookingClassResult)
        {
            ClassCreateResponseDTO dto;
            switch (bookingClassResult.Status)
            {
                case BookingClassStatus.Conflict:
                    dto = new ClassCreateResponseDTO(
                        BookingClassStatus.Conflict,
                        "No class was created");
                    break;
                case BookingClassStatus.Error:
                    dto = new ClassCreateResponseDTO(
                        BookingClassStatus.Error,
                        "An error occurred while creating the class");
                    break;
                case BookingClassStatus.BadRequest:
                    dto = new ClassCreateResponseDTO(
                        BookingClassStatus.BadRequest,
                        "Faulty parameters");
                    break;
                default:
                    dto = new ClassCreateResponseDTO(
                        BookingClassStatus.Success,
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
                    BookingStatus.ClassFull => "Booking failed: The class is already full",
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
                    WaitingListStatus.AlreadySignedUpWL => "Member is already on the waiting list.",
                    WaitingListStatus.AlreadySignedUpMB => "Member is already signed up for the class.",
                    WaitingListStatus.Error => "Failed to add to the waiting list: Unknown error.",
                    _ => "Failed to add to the waiting list: Unknown error."
                };
            }
            return dto;
        }

        public static IEnumerable<LocationListDTO> LocationToLocationListDTO(IEnumerable<Location> locations)
        {
            List<LocationListDTO> locationsDTO = new List<LocationListDTO>();
            foreach (Location location in locations)
            {
                LocationListDTO lDTO = new LocationListDTO
                {
                    LocationId = location.LocationId,
                    StreetName = location.StreetName,
                    HouseNumber = location.HouseNumber,
                    Zipcode = new ZipcodeDTO
                    {
                        ZipcodeNumber = location.Zipcode.ZipcodeNumber,
                        City = new CityDTO
                        {
                            CityName = location.Zipcode.City.CityName,
                            Country = new CountryDTO()
                        }
                    }
                };
                locationsDTO.Add(lDTO);
            }
            return locationsDTO;
        }

        public static IEnumerable<RoomListDTO> RoomToRoomListDTO(IEnumerable<Room> rooms)
        {
            List<RoomListDTO> roomsDTO = new List<RoomListDTO>();
            foreach (Room room in rooms)
            {
                RoomListDTO rlDTO = new RoomListDTO(
                room.RoomId,
                room.RoomName,
                room.Capacity,
                new LocationMinimalDTO(room.Location.LocationId)
                );
                roomsDTO.Add(rlDTO);
            }
            return roomsDTO;
        }

        public static Room RoomMinimalDTOToRoom(RoomMinimalDTO room)
        {
            Room r = new Room
            {
                RoomId = room.RoomId,
                Location = new Location
                {
                    LocationId = room.Location.LocationId
                }
            };
            return r;
        }

        public static IEnumerable<EmployeeListDTO> EmployeeToEmployeeListDTO(IEnumerable<Employee> employees)
        {
            List<EmployeeListDTO> employeesDTO = new List<EmployeeListDTO>();
            foreach (Employee employee in employees)
            {
                EmployeeListDTO eDTO = new EmployeeListDTO(
                employee.User_ID,
                employee.FirstName,
                employee.LastName
            );
                employeesDTO.Add(eDTO);
            }
            return employeesDTO;
        }

        public static Employee EmployeeMinimalDTOToEmployee(EmployeeMinimalDTO employeeDTO)
        {
            Employee employee = new Employee
            {
                User_ID = employeeDTO.User_ID
            };
            return employee;
        }
    }
}
