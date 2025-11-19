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
            IEnumerable<Class>? classes = await _classLogic.GetAllClassesAsync();

            ClassListViewModel? allClassesViewModel = new ClassListViewModel
            {
                Classes = classes?.ToList() ?? new List<Class>()
            };

            return View(allClassesViewModel);
        }


        [HttpPost]
        public async Task<ActionResult> SignUpToClass(int userNumber, int ClassId)
        {
            MemberBookingResponse? result = await _classLogic.signUpAMember(userNumber, ClassId);

            if (result == null)
            {
                TempData["SignUpStatus"] = "error";
                TempData["SignUpMessage"] = "Der opstod en fejl under tilmelding. Prøv igen.";
                return RedirectToAction("Index");
            }

            if (!Enum.TryParse<BookingStatus>(result.Status, ignoreCase: true, out BookingStatus status))
            {
                status = BookingStatus.Error;
            }

            switch (status)
            {
                case BookingStatus.Success:
                    TempData["SignUpStatus"] = "success";
                    TempData["SignUpMessage"] = "Du er nu tilmeldt holdet.";
                    break;

                case BookingStatus.ClassFull:
                    TempData["SignUpStatus"] = "error";
                    TempData["SignUpMessage"] = "Holdet er fuldt. Du kunne ikke tilmeldes.";
                    break;

                case BookingStatus.AlreadySignedUp:
                    TempData["SignUpStatus"] = "error";
                    TempData["SignUpMessage"] = "Du er allerede tilmeldt dette hold.";
                    break;

                case BookingStatus.Error:
                default:
                    TempData["SignUpStatus"] = "error";
                    TempData["SignUpMessage"] = "Der opstod en fejl under tilmelding. Prøv igen.";
                    break;
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
