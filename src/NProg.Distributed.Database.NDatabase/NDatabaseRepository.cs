using System.Collections.Generic;
using System.Linq;
using NDatabase;
using NProg.Distributed.Core.Data;

namespace NProg.Distributed.Database.NDatabase
{
    public sealed class NDatabaseRepository<TKey, TEntity> : IDataRepository<TKey, TEntity>
        where TEntity : class, IIdentifiableEntity<TKey>, new()
    {
        private readonly string dbName;

        public NDatabaseRepository()
        {
            dbName = "OrderDb.ndb";
        }

        #region Implementation of IDataRepository<in TKey,TEntity>

        public TEntity Add(TEntity entity)
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                odb.Store(entity);
                return entity;
            }
        }

        public bool Remove(TEntity entity)
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                var orderToRemove = odb.QueryAndExecute<TEntity>().FirstOrDefault(x => x.EntityId.Equals(entity.EntityId));

                if (orderToRemove == null)
                    return false;

                var oid = odb.GetObjectId(orderToRemove);

                odb.DeleteObjectWithId(oid);
                return true;
            }
        }

        public bool Remove(TKey id)
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                var orderToRemove = odb.QueryAndExecute<TEntity>().FirstOrDefault(x => x.EntityId.Equals(id));

                if (orderToRemove == null)
                    return false;

                var oid = odb.GetObjectId(orderToRemove);

                odb.DeleteObjectWithId(oid);
                return true;
            }
        }

        public TEntity Update(TEntity entity)
        {
            Remove(entity);
            return Add(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                return odb.QueryAndExecute<TEntity>();
            }
        }

        public TEntity Get(TKey id)
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                return odb.QueryAndExecute<TEntity>().FirstOrDefault(x => x.EntityId.Equals(id)) ?? new TEntity();
            }
        }

        #endregion
    }
}
