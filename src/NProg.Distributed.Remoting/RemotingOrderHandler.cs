﻿using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;

namespace NProg.Distributed.Remoting
{
    public class RemotingOrderHandler : MarshalByRefObject, IHandler<Order>
    {
        private readonly NdbOrderDao ndbOrderDao;

        public RemotingOrderHandler()
        {
            ndbOrderDao = new NdbOrderDao("order_remoting.ndb");
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