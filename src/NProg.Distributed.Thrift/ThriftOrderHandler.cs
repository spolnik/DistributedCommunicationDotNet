using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;

namespace NProg.Distributed.Thrift
{
    public class ThriftOrderHandler : SimpleOrderHandler, OrderService.Iface
    {
        public ThriftOrderHandler(IDaoFactory<Order> orderDaoFactory, string dbName)
            : base(orderDaoFactory, dbName)
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