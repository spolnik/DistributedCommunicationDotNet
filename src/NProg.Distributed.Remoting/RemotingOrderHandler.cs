﻿using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;

namespace NProg.Distributed.Remoting
{
    public class RemotingOrderHandler : MarshalByRefObject, IHandler<Guid, Order>
    {
        private readonly IHandler<Guid, Order> ndbOrderDao;

        public RemotingOrderHandler()
        {
            ndbOrderDao = new OrderDaoFactory().CreateDao("order_remoting.ndb");
        }

        public void Add(Guid key, Order item)
        {
            ndbOrderDao.Add(key, item);
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