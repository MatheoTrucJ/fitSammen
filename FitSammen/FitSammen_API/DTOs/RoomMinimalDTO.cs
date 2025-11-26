using System.ComponentModel.DataAnnotations;

namespace FitSammen_API.DTOs
{
    public class RoomMinimalDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "RoomId must be a positive integer.")]
        public int RoomId { get; set; }

        public LocationMinimalDTO Location { get; set; }
        public RoomMinimalDTO() { }
        public RoomMinimalDTO(int roomId, int locationId)
        {
            RoomId = roomId;
            Location = new LocationMinimalDTO(locationId);
        }
    }
}
