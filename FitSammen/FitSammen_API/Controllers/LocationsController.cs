using FitSammen_API.BusinessLogicLayer;
using FitSammen_API.DTOs;
using FitSammen_API.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public ActionResult<IEnumerable<LocationDTO>> GetLocations()
        {
            try
            {
                IEnumerable<LocationDTO> l = _locationService.GetAllLocations();
                return Ok(l);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving locations.");
            }
        }

        [Route("api/locations/{locationId}/rooms")]
        [HttpGet]
        public ActionResult<IEnumerable<RoomDTO>> GetRoomsByLocation(int locationId)
        {
            try
            {
                IEnumerable<RoomDTO> r = _locationService.GetRoomsByLocationId(locationId);
                if (r == null || !r.Any())
                {
                    return NotFound($"No rooms found for location with ID {locationId}.");
                }
                else
                {
                    return Ok(r);
                }

            }
            catch (DataAccessException)
            {
                return StatusCode(500, "An error occurred while retrieving rooms for the specified location.");
            }
        }
    }
}

