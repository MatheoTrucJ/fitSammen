using FitSammen_API.BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitSammen_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public TokensController(ITokenService tokenService) 
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        public ActionResult<string> GetToken()
        {
            try
            {
                string token = _tokenService.GenerateToken();
                return Ok(token);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while generating the token.");
            }
        }
    }
}
