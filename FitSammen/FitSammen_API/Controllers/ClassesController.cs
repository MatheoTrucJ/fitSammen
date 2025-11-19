using FitSammen_API.BusinessLogicLayer;
using FitSammen_API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FitSammen_API.Mapping;
using System.Linq;
using FitSammen_API.Model;

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
    }
}
