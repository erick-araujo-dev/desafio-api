namespace SorteOnlineDesafio.Domain.Interfaces.Commom
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
