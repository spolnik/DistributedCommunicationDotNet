using System;
using Ice;
using NProg.Distributed.Service;
using Order;

namespace NProg.Distributed.Ice
{
    public class IceOrderHandler : OrderServiceDisp_, IHandler<Guid, Domain.Order>
    {
        private readonly IHandler<Guid, Domain.Order> orderDao;

        public IceOrderHandler(IDaoFactory<Guid, Domain.Order> orderDaoFactory, string dbName)
        {
            orderDao = orderDaoFactory.CreateDao(dbName);
        }

        public void Add(Guid key, Domain.Order item)
        {
            orderDao.Add(key, item);
        }

        public Domain.Order Get(Guid guid)
        {
            return orderDao.Get(guid);
        }

        public bool Remove(Guid guid)
        {
            return orderDao.Remove(guid);
        }

        public override void Add(string orderId, OrderDto orderDto, Current current)
        {
            var order = OrderMapper.MapOrder(orderDto);
            Add(order.OrderId, order);
        }

        public override OrderDto Get(string orderId, Current current)
        {
            return OrderMapper.MapOrder(Get(Guid.Parse(orderId)));
        }

        public override bool Remove(string orderId, Current current)
        {
            return Remove(Guid.Parse(orderId));
        }
    }
}