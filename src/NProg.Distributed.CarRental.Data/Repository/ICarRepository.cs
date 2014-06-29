using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.Core.Data;

namespace NProg.Distributed.CarRental.Data.Repository
{
    public interface ICarRepository : IDataRepository<int, Car>
    {
    }
}
