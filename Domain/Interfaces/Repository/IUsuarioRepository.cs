using SorteOnlineDesafio.Domain.Entities;
using SorteOnlineDesafio.Domain.Interfaces.Commom;
using SorteOnlineDesafio.Infra.Repositories;

namespace SorteOnlineDesafio.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario, int>
    {
    }
}
