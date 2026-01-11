using FitSammenWebClient.BusinessLogicLayer;
using FitSammenWebClient.Models;
using FitSammenWebClient.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FitSammenWebClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClassLogic _classLogic;
        private readonly IWaitingListLogic _waitingListLogic;
        private readonly ILoginLogic _loginLogic;


        public HomeController(ILogger<HomeController> logger, IClassLogic classLogic, IWaitingListLogic waitingListLogic, ILoginLogic loginLogic)
        {
            _logger = logger;
            _classLogic = classLogic;
            _waitingListLogic = waitingListLogic;
            _loginLogic = loginLogic;

        }

        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["LoginError"] = "Udfyld venligst både email og adgangskode";
                return RedirectToAction("Index");
            }
            LoginResponseModel? responseModel = await _loginLogic.AuthenticateAndGetToken(loginModel.Email, loginModel.Password);

            if (responseModel == null)
            {
                TempData["LoginError"] = "Ugyldigt brugernavn eller adgangskode. Prøv igen.";
                return RedirectToAction("Index");
            }

            //Gemmer token og userId i Cookie
            var claims = new List<Claim>
            {
                //Gemmer hele JWT'en
                new Claim("AccessToken", responseModel.Token)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false, 
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SignUpToWaitingList(int ClassId)
        {
            string? token = User.FindFirstValue("AccessToken");
            if (string.IsNullOrEmpty(token))
            {
                return Challenge();
            }
            WaitingListEntryResponse? result = await _waitingListLogic.AddMemberToWaitingListAsync(ClassId, token);

            if (result == null)
            {
                TempData["WaitingListStatus"] = "error";
                TempData["WaitingListMessage"] = "Der opstod en fejl under tilmelding til ventelisten. Prøv igen.";
                return RedirectToAction("Index");
            }

            if (!Enum.TryParse<WaitingListStatus>(result.Status.ToString(), ignoreCase: true, out WaitingListStatus status))
            {
                status = WaitingListStatus.Error;
            }

            switch (status)
            {
                case WaitingListStatus.Success:
                    TempData["WaitingListStatus"] = "success";
                    TempData["WaitingListMessage"] = $"Du er nu tilmeldt ventelisten. Din position er {result.WaitingListPosition}.";
                    break;
                case WaitingListStatus.AlreadySignedUpWL:
                    TempData["WaitingListStatus"] = "error";
                    TempData["WaitingListMessage"] = "Du er allerede tilmeldt ventelisten for dette hold.";
                    break;
                case WaitingListStatus.AlreadySignedUpMB:
                    TempData["WaitingListStatus"] = "error";
                    TempData["WaitingListMessage"] = "Du er allerede tilmeldt dette hold og kan ikke tilmeldes ventelisten.";
                    break;
                case WaitingListStatus.Error:
                default:
                    TempData["WaitingListStatus"] = "error";
                    TempData["WaitingListMessage"] = "Der opstod en fejl under tilmelding til ventelisten. Prøv igen.";
                    break;
            }

            return RedirectToAction("Index");

        }


        [HttpPost]
        public async Task<ActionResult> SignUpToClass(int ClassId)
        {
            string? token = User.FindFirstValue("AccessToken");
            if (string.IsNullOrEmpty(token))
            {
                return Challenge();
            }

            MemberBookingResponse? result = await _classLogic.SignUpAMember(ClassId, token);

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
