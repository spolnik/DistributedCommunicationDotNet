using System;
using NProg.Distributed.NDatabase;

namespace NProg.Distributed.Thrift
{
    public class ThriftOrderHandler : SimpleOrderHandler, OrderService.Iface
    {
        public ThriftOrderHandler()
            : base("order_thrift.ndb")
        {
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
    }
}