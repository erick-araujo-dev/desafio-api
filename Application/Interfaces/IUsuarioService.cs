using SorteOnlineDesafio.Domain.Entities;

namespace SorteOnlineDesafio.Application.Interfaces
{
    public interface IUsuarioService
    {
        Usuario createUser(string name, string email, string password);
    }
}
