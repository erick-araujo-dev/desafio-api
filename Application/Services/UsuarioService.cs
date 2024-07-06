using SorteOnlineDesafio.Application.Commom;
using SorteOnlineDesafio.Application.Commom.Exceptions;
using SorteOnlineDesafio.Application.Interfaces;
using SorteOnlineDesafio.Domain.Entities;
using SorteOnlineDesafio.Domain.Interfaces.Repository;

namespace SorteOnlineDesafio.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }   

        public Usuario createUser(string name, string email, string password) 
        {
            validateEmail(email);

            Usuario model = new Usuario
            {
                Nome = name,
                Email = email,
                Senha = password,
                DataCriacao = DateTime.Now,
            };

            var user = _usuarioRepository.AddAndReturnEntity(model);

            return user;
        }

        private void validateEmail(string email)
        {
            var emailValid = Util.ValidateEmail(email);

            if (!emailValid) 
            {
                throw new BusinessException("Formato de e-mail inválido.");
            }

            var userAlreadyCreated = _usuarioRepository.Find(u => u.Email == email).FirstOrDefault();

            if (userAlreadyCreated != null) 
            {
                throw new BusinessException("E-mail já cadastrado.");
            }
        }
    }
}
