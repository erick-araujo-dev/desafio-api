using Microsoft.EntityFrameworkCore.Storage;
using SorteOnlineDesafio.Domain.Interfaces.Commom;

namespace SorteOnlineDesafio.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected ApplicationDbContext _context;
        private IDbContextTransaction _transaction;
        private bool disposed;


        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
        }

        public UnitOfWork()
        {
            _context = new ApplicationDbContext();
        }

        public void BeginTransaction()
        {
            _transaction = this._context.Database.BeginTransaction();
        }

        public void RollbackTransaction()
        {
            this._transaction.Rollback();
        }

        public void CommitTransaction()
        {
            this._transaction.Commit();
        }
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ApplicationDbContext GetContext()
        {
            return this._context;
        }

    }
}

