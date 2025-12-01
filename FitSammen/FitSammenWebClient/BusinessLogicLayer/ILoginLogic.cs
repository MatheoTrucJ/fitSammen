using FitSammenWebClient.Models;

namespace FitSammenWebClient.BusinessLogicLayer
{
    public interface ILoginLogic
    {
        Task<LoginResponseModel?> AuthenticateAndGetToken(string Email, string Password);
    }
}