using FitSammen_API.BusinessLogicLayer;
using FitSammen_API.DTOs;
using FitSammen_API.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitSammen_API.Controllers
{
    [Route("api/classes/{classId}/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {

        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public ActionResult<BookingResponseDTO> CreateBooking(int classId, [FromBody] BookingRequestDTO request)
        {
            var result = _bookingService.BookClass(request.MemberId, classId);

            var dto = ModelConversion.ToBookingResponseDTO(result);

            return result.Status switch
            {
                BookingStatus.Success => Created(string.Empty, dto),
                BookingStatus.ClassFull => Conflict(dto),
                BookingStatus.AlreadySignedUp => Conflict(dto),
                BookingStatus.Error => StatusCode(500, dto),
                _ => StatusCode(500, dto)
            };
        }
    }
}
