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
            BookingClassResult res = _classService.CreateClass(classCreateRequestDTO);
            ClassCreateResponseDTO dto = ModelConversion.ToClassCreateResponseDTO(res);
            switch (dto.Status)
                {
                    case BookingClassStatus.Success:
                        return Ok(dto);
                    case BookingClassStatus.Conflict:
                        return BadRequest(dto.Message);
                    case BookingClassStatus.Error:
                        return StatusCode(500, dto.Message);
                    case BookingClassStatus.BadRequest:
                        return BadRequest(dto.Message);
                    default:
                        return Ok(dto);
                }
        }
    }
}
