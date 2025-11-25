using FitSammen_API.BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitSammen_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetLocations()
        {
            IEnumerable<string> locations = _locationService.GetAllLocations();
            return Ok(locations);
        }

        [Route("api/locations/{locationId}/rooms")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetRoomsByLocation(int locationId)
        {
            IEnumerable<string> rooms = _locationService.GetRoomsByLocation(locationId);
            return Ok(rooms);
        }
    }
}
