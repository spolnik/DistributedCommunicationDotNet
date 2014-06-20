using NProg.Distributed.OrderService.Api;
using NProg.Distributed.OrderService.Database;

namespace NProg.Distributed.NDatabase
{
    public sealed class InMemoryOrderDaoFactory : IOrderDaoFactory
    {
        public IOrderApi CreateDao()
        {
            return new InMemoryDao();
        }
    }
}