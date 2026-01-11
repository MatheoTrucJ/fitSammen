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
            if (dto is null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest("Email and password are required.");
            }

            try
            {
                string token = _tokenService.CreateToken(dto.Email!, dto.Password!);
                return Ok(new LoginResponseDto { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (FitSammen_API.Exceptions.DataAccessException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception)
            {

                return StatusCode(500, "Error");
            }
        }
    }
}
