using FitSammen_API.BusinessLogicLayer;
using FitSammen_API.DTOs;
using FitSammen_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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

        [HttpPost]
        public ActionResult<LoginResponseDto> CreateToken([FromBody] LoginRequestDTO dto)
        {
            try
            {
                string foundToken;

                bool hasInput = ((!string.IsNullOrWhiteSpace(dto.Email)) && (!string.IsNullOrWhiteSpace(dto.Password)));

                if (hasInput)
                {
                    foundToken = _tokenService.CreateToken(dto.Email!, dto.Password!);
                    return Ok(new LoginResponseDto { Token = foundToken });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch {
                return StatusCode(500, "Error");
            }
        }
    }
}
