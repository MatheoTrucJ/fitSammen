using FitSammen_API.Model;

namespace FitSammen_API.BusinessLogicLayer
{
    public interface ITokenService
    {
        string CreateToken(string email, string password);
        User? FindUserByEmailAndPassword(string email, string password);
    }
}
