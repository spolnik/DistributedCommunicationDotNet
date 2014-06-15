using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.NDatabase
{
    public class OrderDaoFactory : IDaoFactory<Order>
    {
        public IHandler<Order> CreateDao(string dbName)
        {
            return new InMemoryOrderDao();
//            return new NdbOrderDao(dbName);
        }
    }
}