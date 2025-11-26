using System.ComponentModel.DataAnnotations;

namespace FitSammen_API.DTOs
{
    public class RoomMinimalDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "RoomId must be a positive integer.")]
        public int RoomId { get; set; }
        public RoomMinimalDTO(int roomId)
        {
            RoomId = roomId;
        }
    }
}
