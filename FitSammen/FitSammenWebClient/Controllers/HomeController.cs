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
        public async Task<ActionResult> SignUpToClass(int userNumber, int ClassId)
        {
            var user = new Member
            {
                User_Id = userNumber
            };

            Boolean result = false;
            //IEnumerable<Class>? classes = await _classLogic.GetAllClassesAsync(ClassId);
            //Class currentClass = classes.ElementAt(0);
            
            result = await _classLogic.signUpAMember(userNumber, ClassId);

            if (result)
            {
                TempData["SignUpStatus"] = "success";
                TempData["SignUpMessage"] = "Du er nu tilmeldt holdet.";
            }
            else
            {
                TempData["SignUpStatus"] = "error";
                TempData["SignUpMessage"] = "Tilmelding mislykkedes. Holdet Er fuldt.";
            }

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
