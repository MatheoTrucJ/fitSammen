using FitSammen_API.BusinessLogicLayer;
using FitSammen_API.DTOs;
using FitSammen_API.Hubs;
using FitSammen_API.Mapping;
using FitSammen_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace FitSammen_API.Controllers
{
    [Route("api/classes/{classId}/bookings")]
    [ApiController]
    [Authorize]
    public class BookingsController : ControllerBase
    {

        private readonly IBookingService _bookingService;
        private readonly IHubContext<ClassHub> _classHub;

        public BookingsController(IBookingService bookingService, IHubContext<ClassHub> classHub)
        {
            _bookingService = bookingService;
            _classHub = classHub;
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserType.Member))]
        public async Task<ActionResult<BookingResponseDTO>> CreateBooking(int classId)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("User identifier not found.");
            }

            int userId = int.Parse(userIdString);

            BookingResult? result = _bookingService.BookClass(userId, classId);

            BookingResponseDTO dto = ModelConversion.ToBookingResponseDTO(result);

            if (result.Status == BookingStatus.Success)
            {
                // her sker signalR push notifikationen
                await _classHub.Clients.All.SendAsync(
                "MemberSignUpToClass",
                 new { ClassId = classId, IncrementMemberCount = 1 }
                );
            }

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
