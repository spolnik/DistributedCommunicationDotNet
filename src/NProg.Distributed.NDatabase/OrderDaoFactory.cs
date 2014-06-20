using NProg.Distributed.Domain;
using NProg.Distributed.Domain.Api;

namespace NProg.Distributed.NDatabase
{
    public class OrderDaoFactory : IOrderDaoFactory
    {
        public IOrderApi CreateDao(string dbName)
        {
            return new InMemoryDao();
//            return new NdbOdbDao(dbName);
        }
    }
}