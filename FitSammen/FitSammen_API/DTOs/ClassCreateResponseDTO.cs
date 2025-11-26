using FitSammen_API.BusinessLogicLayer;

namespace FitSammen_API.DTOs
{
    public class ClassCreateResponseDTO
    {
        public BookingClassStatus Status { get; set; }
        public string Message { get; set; }

        public ClassCreateResponseDTO(BookingClassStatus status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
