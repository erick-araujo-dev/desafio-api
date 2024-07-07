using Microsoft.AspNetCore.Mvc;
using SorteOnlineDesafio.Application.Interfaces;
using SorteOnlineDesafio.Application.Models;
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
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController
        (
            IAuthService authService,
            IUsuarioRepository usuarioRepository,
            ITokenService tokenService,
            IUserService userService
        )
        {
            _authService = authService;
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
            _userService = userService;
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
                var userEntitie = _usuarioRepository.Find(u => u.Email == request.Email).FirstOrDefault();

                var userModel = new UserModel
                {
                    UserId = userEntitie.UsuarioId,
                    Name = userEntitie.Nome,
                    Email = userEntitie.Email,
                };

                string token = _tokenService.GenerateToken(userModel);

                SignInResponse response = new()
                {
                    Token = token,
                    UserId = userModel.UserId
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
                var userCreated = _userService.CreateUser(request.Name, request.Email, request.Password);

                string token = _tokenService.GenerateToken(userCreated);

                SignInResponse response = new()
                {
                    Token = token,
                    UserId = userCreated.UserId
                };

                return Created($"api/user/{userCreated.UserId}", response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
