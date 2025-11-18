using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberBookingController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateBooking([FromBody] CreateBookingDto dto)
        {
            // Hvis alt går godt:
            return Ok(new { Message = "Booking created successfully." });
        }
    }

    public class CreateBookingDto
    {
        public int MemberId { get; set; }
        public int ClassId { get; set; }
    }
}
