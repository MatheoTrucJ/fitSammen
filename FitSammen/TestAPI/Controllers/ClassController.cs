using FitSammenWebClient.Models;
using Microsoft.AspNetCore.Mvc;

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
            Employee instructor1 = new Employee("Kim", "Kimmer", "kim@12.dk", "23232323", DateOnly.FromDateTime(DateTime.Now), 3, UserType.Employee, "230802-2342");
            Location location1 = new Location("TestGade", 10, 9000, "Aalborg", "Danmark");
            Room room1 = new Room(1, "Room1", 2, location1);
            Class Class1 = new Class(1, DateOnly.FromDateTime(DateTime.Now), instructor1,"TEST", room1, "YOGA", 3, 120, TimeOnly.FromDateTime(DateTime.Now), ClassType.Yoga);

            Employee instructor2 = new Employee("Kim", "Kimmer", "kim@12.dk", "23232323", DateOnly.FromDateTime(DateTime.Now), 3, UserType.Employee, "230802-2342");
            Location location2 = new Location("TestGade", 10, 9000, "Aalborg", "Danmark");
            Room room2 = new Room(1, "Room1", 2, location2);
            Class Class2 = new Class(1, DateOnly.FromDateTime(DateTime.Now), instructor2, "TEST", room2, "YOGA", 3, 120, TimeOnly.FromDateTime(DateTime.Now), ClassType.Yoga);

            var classes = new List<Class> { Class1, Class2 };
            return Ok(classes);
        }

        // GET api/<ClassController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Employee instructor1 = new Employee("Kim", "Kimmer", "kim@12.dk", "23232323", DateOnly.FromDateTime(DateTime.Now), 3, UserType.Employee, "230802-2342");
            Location location1 = new Location("TestGade", 10, 9000, "Aalborg", "Danmark");
            Room room1 = new Room(1, "Room1", 2, location1);
            Class Class1 = new Class(1, DateOnly.FromDateTime(DateTime.Now), instructor1, "TEST", room1, "YOGA", 3, 120, TimeOnly.FromDateTime(DateTime.Now), ClassType.Yoga);

            return Ok(Class1);
        }
    }
}
