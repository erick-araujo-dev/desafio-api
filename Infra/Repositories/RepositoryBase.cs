using Microsoft.EntityFrameworkCore;
using SorteOnlineDesafio.Domain.Interfaces.Commom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace SorteOnlineDesafio.Infra.Repositories
{
    public class RepositoryBase<TEntity, TKey> : IDisposable, IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        internal readonly ApplicationDbContext _context = null;
        private readonly IUnitOfWork _uow;
        private DbSet<TEntity> _entities;
        string errorMessage = string.Empty;
        private bool disposed;

        public RepositoryBase(ApplicationDbContext context)
        {
            this._context = context;
            _entities = _context.Set<TEntity>();
        }

        public RepositoryBase(IUnitOfWork uow)
        {
            this._context = ((UnitOfWork)uow).GetContext();
            _entities = _context.Set<TEntity>();
            this._uow = uow;
        }

        public void Add(TEntity obj)
        {
            _entities.Add(obj);
            _context.SaveChanges();
        }

        public TEntity AddAndReturnEntity(TEntity obj)
        {
            _entities.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public void Update(TEntity obj)
        {
            _entities.Attach(obj);
            _context.SaveChanges();
        }

        public void Delete(TEntity obj)
        {
            _entities.Remove(obj);
            _context.SaveChanges();
        }

        public TEntity Get(TKey id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> All()
        {
            return _entities.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return _entities.Where(predicate);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this._uow?.Dispose();
                }
            }
            disposed = true;
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }
    }
}
