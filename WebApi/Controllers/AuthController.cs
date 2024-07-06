using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SorteOnlineDesafio.Application.Services;
using SorteOnlineDesafio.WebApi.Models.Request;

namespace SorteOnlineDesafio.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (_authService.VerifyPassword(request.Email, request.Password))
            {
                return Ok("Login successful");
            }
            else
            {
                return Unauthorized("Invalid credentials");
            }
        }
    }
}
