using FitSammen_API.DatabaseAccessLayer;
using FitSammen_API.Exceptions;
using FitSammen_API.Model;
using FitSammen_API.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FitSammen_API.BusinessLogicLayer
{
    public class TokenService : ITokenService
    {
        private readonly IMemberAccess _memberAccess;

        private readonly IConfiguration _configuration;

        private readonly SecurityHelper _security;

        public TokenService(IMemberAccess memberAccess, IConfiguration configuration)
        {
            _memberAccess = memberAccess;
            _security = new SecurityHelper(configuration);
            _configuration = configuration;
        }

        public string CreateToken(string email, string password)
        {
            try
            {
                User user = _memberAccess.FindUserByEmailAndPassword(email, password);

                SymmetricSecurityKey? signingKey = _security.GetSecurityKey();
                if (signingKey is null)
                {
                    throw new DataAccessException("Signing key failed");
                }

                string jwtToken = GenerateToken(user, signingKey);

                return jwtToken;
            }
            catch (Exception)
            {
                throw new DataAccessException("Error creating token.");
            }
        }

        private string GenerateToken(User user, SymmetricSecurityKey SIGNING_KEY)
        {
            string jwtString;

            SigningCredentials credentials = new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.User_ID.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname,  user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            };

            int durationInMinutes = 60;
            DateTime expireAt = DateTime.Now.AddMinutes(durationInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"], 
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            jwtString = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtString;
        }

        public User FindUserByEmailAndPassword(string email, string password)
        {
            try
            {
                User user = _memberAccess.FindUserByEmailAndPassword(email, password);

                if (user is null)
                {
                    throw new DataAccessException("User not found.");
                }

                return user;
            }
            catch (Exception)
            {
                throw new DataAccessException("Error retrieving user from the database.");
            }
        }
    }
}
