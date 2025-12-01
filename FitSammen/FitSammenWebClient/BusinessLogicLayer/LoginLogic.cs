using FitSammenWebClient.Models;
using FitSammenWebClient.ServiceLayer;

namespace FitSammenWebClient.BusinessLogicLayer
{
    public class LoginLogic : ILoginLogic
    {
        private readonly ILoginService _loginService;

        public LoginLogic(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<LoginResponseModel?> AuthenticateAndGetToken(string Email, string Password)
        {
            LoginResponseModel? response = await _loginService.GetTokenFromApiAsync(Email, Password);

            return response;
        }
    }
}