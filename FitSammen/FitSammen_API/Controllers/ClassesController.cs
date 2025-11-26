using FitSammen_API.BusinessLogicLayer;
using FitSammen_API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FitSammen_API.Mapping;
using System.Linq;
using FitSammen_API.Model;
using FitSammen_API.Exceptions;

namespace FitSammen_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClassListItemDTO>> GetAvailableClasses()
        {
            IEnumerable<Class> classes = _classService.GetUpcomingClasses();

            List<ClassListItemDTO> dtoList = classes
                .Select(c => ModelConversion.ToClassListItemDTO(c))
                .ToList();

            return Ok(dtoList);
        }

        [HttpPost]
        public ActionResult<ClassCreateResponseDTO> CreateClass([FromBody] ClassCreateRequestDTO classCreateRequestDTO)
        {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Class cls = ModelConversion.ClassCreateRequestDTOToClass(classCreateRequestDTO);
                BookingClassResult res = _classService.CreateClass(cls);
                ClassCreateResponseDTO dto = ModelConversion.ToClassCreateResponseDTO(res);
            switch (res.Status)
                {
                    case ClassCreateStatus.Success:
                        return Ok(res);
                    case ClassCreateStatus.Conflict:
                        return BadRequest("There was a conflict and no room was booked");
                    case ClassCreateStatus.Error:
                        return StatusCode(500, "An error occured");
                    case ClassCreateStatus.BadRequest:
                        return BadRequest("Bad request");
                    default:
                        return Ok(res);
                }
        }
    }
}
