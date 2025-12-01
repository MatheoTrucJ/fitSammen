using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FitSammen_API.Security
{
    public class SecurityHelper
    {
        private readonly IConfiguration _configuration;

        public SecurityHelper(IConfiguration inConfiguration)
        {
            _configuration = inConfiguration;
        }

        public SymmetricSecurityKey? GetSecurityKey()
        {
            SymmetricSecurityKey? SIGNING_KEY = null;
            string? SECRET_KEY = _configuration["JwtSettings:SecretKey"];
            if (!string.IsNullOrEmpty(SECRET_KEY))
            {
                SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
            }
            return SIGNING_KEY;
        }
    }
}
