using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.NDatabase
{
    public class OrderDaoFactory : IDaoFactory<Guid, Order>
    {
        public IHandler<Guid, Order> CreateDao(string dbName)
        {
            return new InMemoryDao<Guid,Order>();
//            return new NdbOdbDao(dbName);
        }
    }
}