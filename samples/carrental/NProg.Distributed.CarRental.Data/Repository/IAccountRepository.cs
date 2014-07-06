using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.Core.Data;

namespace NProg.Distributed.CarRental.Data.Repository
{
    public interface IAccountRepository : IDataRepository<int, Account>
    {
        Account GetByLogin(string login);
    }
}
