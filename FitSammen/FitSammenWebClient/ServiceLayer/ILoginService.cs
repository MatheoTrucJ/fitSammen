using FitSammenWebClient.Models;

namespace FitSammenWebClient.ServiceLayer
{
    public interface ILoginService
    {
        Task<LoginResponseModel?> GetTokenFromApiAsync(string email, string password);
    }
}