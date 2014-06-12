using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;

namespace NProg.Distributed.Msmq
{
    public class MsmqOrderHandler : IHandler<Order>
    {
        private readonly NdbOrderDao ndbOrderDao;

        public MsmqOrderHandler()
        {
            ndbOrderDao = new NdbOrderDao("order_msmq.ndb");
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