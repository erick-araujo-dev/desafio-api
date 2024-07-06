using Microsoft.AspNetCore.Mvc;
using SorteOnlineDesafio.Domain.Entities;
using SorteOnlineDesafio.Domain.Interfaces.Repository;
using SorteOnlineDesafio.Infra.Repositories;

namespace SorteOnlineDesafio.WebApi.Controllers
{
        [ApiController]
        [Route("api/usuario")]
        public class UsuarioController : ControllerBase
        {
            private readonly IUsuarioRepository _usuarioRepository;

            public UsuarioController(IUsuarioRepository usuarioRepository)
            {
                _usuarioRepository = usuarioRepository;
            }

            [HttpGet]
            public ActionResult GetUsuarios()
            {
                try
                {
                    var usuarios = _usuarioRepository.All();
                    return Ok(usuarios);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erro ao obter usuários: {ex.Message}");
                }
            }
        }
}
