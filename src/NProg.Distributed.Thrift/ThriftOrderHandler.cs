using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.Thrift
{
    public class ThriftOrderHandler : SimpleHandler<Guid, Order>, MessageService.Iface
    {
        public ThriftOrderHandler(IDaoFactory<Guid, Order> orderDaoFactory, string dbName)
            : base(orderDaoFactory, dbName)
        {
        }

        public void Add(string key, ThriftOrder thriftOrder)
        {
            var order = OrderMapper.MapOrder(thriftOrder);
            Add(order.OrderId, order);
        }

        public ThriftOrder Get(string orderId)
        {
            return OrderMapper.MapOrder(Get(Guid.Parse(orderId)));
        }

        public bool Remove(string orderId)
        {
            return Remove(Guid.Parse(orderId));
        }

        public ThriftMessage Send(ThriftMessage message)
        {
            throw new NotImplementedException();
        }
    }
}