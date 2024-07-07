using SorteOnlineDesafio.Application.Commom.Exceptions;
using SorteOnlineDesafio.Application.Interfaces;
using SorteOnlineDesafio.Domain.Interfaces.Repository;

namespace SorteOnlineDesafio.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public bool VerifyPassword(string email, string plainPassword)
        {
            //Lanca NotFoundException quando usuario nao for encontrado
            var usuario = _usuarioRepository.Find(u => u.Email == email).FirstOrDefault() ?? throw new NotFoundException("Usuário inválido.");

            return BCrypt.Net.BCrypt.Verify(plainPassword, usuario.Senha);
        }
    }
}
