using System.Collections.Concurrent;
using System.Collections.Generic;
using NProg.Distributed.Core.Data;

namespace NProg.Distributed.Database.InMemory
{
    public sealed class InMemoryRepository<TKey, TEntity> : IDataRepository<TKey, TEntity> 
        where TEntity : class, IIdentifiableEntity<TKey>, new()
    {
        private static readonly ConcurrentDictionary<TKey, TEntity> InMemoryDb = new ConcurrentDictionary<TKey, TEntity>();

        #region Implementation of IDataRepository<in TKey,TEntity>

        public TEntity Add(TEntity entity)
        {
            return InMemoryDb.AddOrUpdate(entity.EntityId, entity, (guid, order) => order);
        }

        public bool Remove(TEntity entity)
        {
            TEntity value;
            return InMemoryDb.TryRemove(entity.EntityId, out value);
        }

        public bool Remove(TKey id)
        {
            TEntity value;
            return InMemoryDb.TryRemove(id, out value);
        }

        public TEntity Update(TEntity entity)
        {
            return InMemoryDb.AddOrUpdate(entity.EntityId, entity, (guid, order) => entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return InMemoryDb.Values;
        }

        public TEntity Get(TKey id)
        {
            TEntity value;
            InMemoryDb.TryGetValue(id, out value);

            return value ?? new TEntity();
        }

        #endregion
    }
}