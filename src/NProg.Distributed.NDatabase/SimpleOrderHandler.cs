using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.NDatabase
{
    public class SimpleOrderHandler : IHandler<Order>
    {
        private readonly NdbOrderDao ndbOrderDao;

        public SimpleOrderHandler(string dbName)
        {
            ndbOrderDao = new NdbOrderDao(dbName);
        }

        public void Add(Order item)
        {
            ndbOrderDao.Add(item);
        }

        public Order Get(Guid guid)
        {
            return ndbOrderDao.Get(guid);
        }

        public bool Remove(Guid guid)
        {
            return ndbOrderDao.Remove(guid);
        }
    }
}