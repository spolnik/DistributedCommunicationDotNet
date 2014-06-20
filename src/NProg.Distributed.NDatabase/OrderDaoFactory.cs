using NProg.Distributed.OrderService.Api;
using NProg.Distributed.OrderService.Database;

namespace NProg.Distributed.NDatabase
{
    public sealed class OrderDaoFactory : IOrderDaoFactory
    {
        public IOrderApi CreateDao(string dbName)
        {
            if (dbName != null && dbName.EndsWith(".ndb"))
            {
                return new NdbOdbDao(dbName);
            }

            return new InMemoryDao();
        }
    }
}