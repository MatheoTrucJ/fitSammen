using FitSammenWebClient.BusinessLogicLayer;
using FitSammenWebClient.Models;
using FitSammenWebClient.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FitSammenWebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ClassLogic _classLogic;

        public HomeController(ILogger<HomeController> logger, IConfiguration inConfiguration)
        {
            _logger = logger;
            _classLogic = new ClassLogic(inConfiguration);
        }

        public async Task<IActionResult> Index()
        {
            var classes = await _classLogic.GetAllClassesAsync();

            var allClassesViewModel = new ClassListViewModel
            {
                Classes = classes.ToList()
            };

            return View(allClassesViewModel);
        }


        [HttpPost]
        public async Task<ActionResult> SignUpToClass(Member User, int ClassId)
        {
            User.UserNumber = 2;

            Boolean result = false;
            IEnumerable<Class> classes = await _classLogic.GetAllClassesAsync(ClassId);
            var currentClass = classes.ElementAt(0);
            
            result = await _classLogic.signUpAMember(User, currentClass);

            return RedirectToAction("Index");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
