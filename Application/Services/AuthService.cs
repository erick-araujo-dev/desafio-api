using Microsoft.EntityFrameworkCore;
using SorteOnlineDesafio.Domain.Interfaces.Repository;

namespace SorteOnlineDesafio.Application.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public bool VerifyPassword(string email, string plainPassword)
        {
            var usuario = _usuarioRepository.Find(u => u.Email == email).FirstOrDefault();
            if (usuario == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(plainPassword, usuario.Senha);
        }
    }
}
