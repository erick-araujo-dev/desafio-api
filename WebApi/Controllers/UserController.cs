using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SorteOnlineDesafio.Application.Interfaces;
using SorteOnlineDesafio.WebApi.Commom;

namespace SorteOnlineDesafio.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            try
            {
                var usuarios = _userService.GetAll();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetById(int userId)
        {
            try
            {
                var usuarios = _userService.GetById(userId);

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


    }
}
