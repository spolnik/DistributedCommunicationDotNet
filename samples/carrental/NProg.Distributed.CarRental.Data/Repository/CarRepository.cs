using System.Collections.Generic;
using System.Linq;
using NProg.Distributed.CarRental.Domain;

namespace NProg.Distributed.CarRental.Data.Repository
{
    public class CarRepository : DataRepositoryBase<Car>, ICarRepository
    {
        protected override Car AddEntity(CarRentalContext entityContext, Car entity)
        {
            return entityContext.CarSet.Add(entity);
        }

        protected override Car UpdateEntity(CarRentalContext entityContext, Car entity)
        {
            return (from e in entityContext.CarSet
                    where e.CarId == entity.CarId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Car> GetEntities(CarRentalContext entityContext)
        {
            return from e in entityContext.CarSet
                   select e;
        }

        protected override Car GetEntity(CarRentalContext entityContext, int id)
        {
            var query = (from e in entityContext.CarSet
                         where e.CarId == id
                         select e);

            return query.FirstOrDefault();
        }
    }
}
