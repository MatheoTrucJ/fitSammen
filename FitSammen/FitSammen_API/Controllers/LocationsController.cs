using FitSammen_API.BusinessLogicLayer;
using FitSammen_API.DTOs;
using FitSammen_API.Exceptions;
using FitSammen_API.Mapping;
using FitSammen_API.Model;
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
        public ActionResult<IEnumerable<LocationListDTO>> GetLocations()
        {
            try
            {
                IEnumerable<Location> l = _locationService.GetAllLocations();
                IEnumerable<LocationListDTO> lDTO = ModelConversion.LocationToLocationListDTO(l);
                return Ok(lDTO);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving locations.");
            }
        }

        [Route("api/[controller]/{locationId}/rooms")]
        [HttpGet]
        public ActionResult<IEnumerable<RoomListDTO>> GetRoomsByLocation(int locationId)
        {
            try
            {
                IEnumerable<Room> r = _locationService.GetRoomsByLocationId(locationId);
                IEnumerable<RoomListDTO> rDTO = ModelConversion.RoomToRoomListDTO(r);
                if (r == null || !r.Any())
                {
                    return NotFound($"No rooms found for location with ID {locationId}.");
                }
                else
                {
                    return Ok(rDTO);
                }

            }
            catch (DataAccessException)
            {
                return StatusCode(500, "An error occurred while retrieving rooms for the specified location.");
            }
        }

        [Route("api/locations/{locationId}/employees")]
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeListDTO>> GetEmployeesByLocation(int locationId)
        {
            try
            {
                IEnumerable<Employee> e = _locationService.GetEmployeesByLocationId(locationId);
                IEnumerable<EmployeeListDTO> eDTO = ModelConversion.EmployeeToEmployeeListDTO(e);
                if (e == null || !e.Any())
                {
                    return NotFound($"No rooms found for location with ID {locationId}.");
                }
                else
                {
                    return Ok(eDTO);
                }

            }
            catch (DataAccessException)
            {
                return StatusCode(500, "An error occurred while retrieving rooms for the specified location.");
            }
        }
    }
}

