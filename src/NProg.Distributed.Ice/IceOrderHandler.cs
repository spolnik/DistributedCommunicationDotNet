using System;
using Ice;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;
using Order;

namespace NProg.Distributed.Ice
{
    public class IceOrderHandler : OrderServiceDisp_, IHandler<Domain.Order>
    {
        private readonly IHandler<Domain.Order> orderDao;

        public IceOrderHandler(IDaoFactory<Domain.Order> orderDaoFactory, string dbName)
        {
            orderDao = orderDaoFactory.CreateDao(dbName);
        }

        public void Add(Domain.Order item)
        {
            orderDao.Add(item);
        }

        public Domain.Order Get(Guid guid)
        {
            return orderDao.Get(guid);
        }

        public bool Remove(Guid guid)
        {
            return orderDao.Remove(guid);
        }

        public override void Add(OrderDto order, Current current__)
        {
            Add(OrderMapper.MapOrder(order));
        }

        public override OrderDto Get(string orderId, Current current__)
        {
            return OrderMapper.MapOrder(Get(Guid.Parse(orderId)));
        }

        public override bool Remove(string orderId, Current current__)
        {
            return Remove(Guid.Parse(orderId));
        }
    }
}