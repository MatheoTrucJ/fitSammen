using FitSammen_API.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestAPI.Controllers
{   //https://localhost:7033/api/Class/
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        // GET: api/<ClassController>
        [HttpGet]
        public IActionResult Get()
        {
            Class Class1 = new Class();
            Class Class2 = new Class();
            Class1.Id = 1;
            Class2.Id = 2;
            var classes = new List<Class> { Class1, Class2 };
            return Ok(classes);
        }

        // GET api/<ClassController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Class Class1 = new Class();
            Class1.Id = id;
            return Ok(Class1);
        }
    }
}
