using Microsoft.AspNetCore.Mvc;
using SorteOnlineDesafio.Application.Interfaces;
using SorteOnlineDesafio.Domain.Interfaces.Repository;
using SorteOnlineDesafio.WebApi.Commom;
using SorteOnlineDesafio.WebApi.Models.Request;
using SorteOnlineDesafio.WebApi.Models.Response;

namespace SorteOnlineDesafio.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;

        public AuthController
        (
            IAuthService authService,
            IUsuarioRepository usuarioRepository,
            ITokenService tokenService,
            IUsuarioService usuarioService
        )
        {
            _authService = authService;
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
            _usuarioService = usuarioService;
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] SigninRequest request)
        {
            try
            {
                bool isAuthenticated = _authService.VerifyPassword(request.Email, request.Password);

                if (!isAuthenticated)
                {
                    return Unauthorized("Credencias invalidas");
                }

                //Aqui nunca vai ser null pq o metodo VerifyPassword lanca uma NotFoundException quando nao encontra user pelo email
                var user = _usuarioRepository.Find(u => u.Email == request.Email).FirstOrDefault();

                string token = _tokenService.GenerateToken(user);

                SignInResponse response = new SignInResponse
                {
                    Token = token,
                    UserId = user.UsuarioId
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] SignUpRequest request)
        {
            try
            {
                var userCreated = _usuarioService.createUser(request.Name, request.Email, request.Password);

                string token = _tokenService.GenerateToken(userCreated);

                SignInResponse response = new SignInResponse
                {
                    Token = token,
                    UserId = userCreated.UsuarioId
                };

                return Created($"/user/{userCreated.UsuarioId}", response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
