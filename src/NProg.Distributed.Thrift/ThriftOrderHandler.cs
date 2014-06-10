using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;

namespace NProg.Distributed.Thrift
{
    public class ThriftOrderHandler : OrderService.Iface, IHandler<Order>
    {
        private readonly NdbOrderDao ndbOrderDao;

        public ThriftOrderHandler()
        {
            ndbOrderDao = new NdbOrderDao("order_thrift.ndb");
        }

        public void Add(ThriftOrder order)
        {
            Add(OrderMapper.MapOrder(order));
        }

        public ThriftOrder Get(string orderId)
        {
            return OrderMapper.MapOrder(Get(Guid.Parse(orderId)));
        }

        public bool Remove(string orderId)
        {
            return Remove(Guid.Parse(orderId));
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