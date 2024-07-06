using SorteOnlineDesafio.Domain.Entities;

namespace SorteOnlineDesafio.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Usuario user);
    }
}
