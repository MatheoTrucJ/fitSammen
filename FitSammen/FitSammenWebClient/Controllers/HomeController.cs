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

            MemberBookingResponse result = null;

            result = await _classLogic.signUpAMember(userNumber, ClassId);

            // result.Status er en string, fx "Success"
            if (!Enum.TryParse<BookingStatus>(result.Status, ignoreCase: true, out var status))
            {
                // Hvis vi ikke kan parse, betragter vi det som en fejl
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
