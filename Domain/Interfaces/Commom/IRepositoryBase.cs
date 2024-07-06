using System.Linq.Expressions;

namespace SorteOnlineDesafio.Domain.Interfaces.Commom
{
    public interface IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        void Add(TEntity entity);
        TEntity AddAndReturnEntity(TEntity obj);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity Get(TKey id);
        TEntity Get(Int32 id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> All();
    }
}
