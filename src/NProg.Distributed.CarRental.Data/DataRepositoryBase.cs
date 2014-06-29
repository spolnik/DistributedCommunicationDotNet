using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NProg.Distributed.CarRental.Data.Utils;
using NProg.Distributed.Core.Data;

namespace NProg.Distributed.CarRental.Data
{
    public abstract class DataRepositoryBase<TEntity, TContext> : IDataRepository<int, TEntity>
        where TEntity : class, IIdentifiableEntity<int>, new()
        where TContext : DbContext, new()
    {
        protected abstract TEntity AddEntity(TContext entityContext, TEntity entity);

        protected abstract TEntity UpdateEntity(TContext entityContext, TEntity entity);

        protected abstract IEnumerable<TEntity> GetEntities(TContext entityContext);

        protected abstract TEntity GetEntity(TContext entityContext, int id);

        public TEntity Add(TEntity entity)
        {
            using (var entityContext = new TContext())
            {
                TEntity addedEntity = AddEntity(entityContext, entity);
                entityContext.SaveChanges();
                return addedEntity;
            }
        }

        public bool Remove(TEntity entity)
        {
            using (var entityContext = new TContext())
            {
                entityContext.Entry(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
                return true;
            }
        }

        public bool Remove(int id)
        {
            using (var entityContext = new TContext())
            {
                TEntity entity = GetEntity(entityContext, id);
                entityContext.Entry(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
                return true;
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var entityContext = new TContext())
            {
                TEntity existingEntity = UpdateEntity(entityContext, entity);

                SimpleMapper.PropertyMap(entity, existingEntity);

                entityContext.SaveChanges();
                return existingEntity;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (var entityContext = new TContext())
                return (GetEntities(entityContext)).ToArray().ToList();
        }

        public TEntity Get(int id)
        {
            using (var entityContext = new TContext())
                return GetEntity(entityContext, id);
        }
    }

    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, CarRentalContext>
       where T : class, IIdentifiableEntity<int>, new()
    {
    }
}