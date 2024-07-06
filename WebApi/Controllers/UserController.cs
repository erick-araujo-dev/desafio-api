using Microsoft.AspNetCore.Mvc;
using SorteOnlineDesafio.Domain.Entities;
using SorteOnlineDesafio.Domain.Interfaces.Repository;
using SorteOnlineDesafio.Infra.Repositories;
using SorteOnlineDesafio.WebApi.Commom;

namespace SorteOnlineDesafio.WebApi.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UserController : BaseController
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UserController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        [Route("/all")]

        public IActionResult GetAll()
        {
            try
            {
                var usuarios = _usuarioRepository.All();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("/{id}")]
        public IActionResult GetById([FromQuery] int id)
        {
            try
            {
                var usuarios = _usuarioRepository.Find(u => u.UsuarioId == id).FirstOrDefault();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


    }
}
