namespace FitSammen_API.DTOs
{
    public class RoomDTO
    {
        public int? RoomId { get; set; } = null;
        public string? RoomName { get; set; } = null;
        public int? Capacity { get; set; } = null;
        public LocationDTO? LocationDTO { get; set; } = null;
    }
}
