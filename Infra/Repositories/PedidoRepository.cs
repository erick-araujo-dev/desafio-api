using SorteOnlineDesafio.Domain.Entities;
using SorteOnlineDesafio.Domain.Interfaces.Commom;
using SorteOnlineDesafio.Domain.Interfaces.Repository;

namespace SorteOnlineDesafio.Infra.Repositories
{
    public class PedidoRepository : RepositoryBase<Pedido, int>, IPedidoRepository
    {
        public PedidoRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    }
}
