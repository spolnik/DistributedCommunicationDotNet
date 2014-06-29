using System.Collections.Generic;

namespace NProg.Distributed.Core.Data
{
    public interface IDataRepository
    {
    }

    public interface IDataRepository<in TKey, TEntity> : IDataRepository
        where TEntity : class, IIdentifiableEntity<TKey>, new()
    {
        TEntity Add(TEntity entity);

        bool Remove(TEntity entity);

        bool Remove(TKey id);

        TEntity Update(TEntity entity);

        IEnumerable<TEntity> GetAll();

        TEntity Get(TKey id);
    }
}