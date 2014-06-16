using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.NDatabase
{
    public class SimpleOrderHandler : IHandler<Order>
    {
        private readonly IHandler<Order> orderDao;

        public SimpleOrderHandler(IDaoFactory<Order> orderDaoFactory, string dbName)
        {
            orderDao = orderDaoFactory.CreateDao(dbName);
        }

        public void Add(Order item)
        {
            orderDao.Add(item);
        }

        public Order Get(Guid guid)
        {
            return orderDao.Get(guid);
        }

        public bool Remove(Guid guid)
        {
            return orderDao.Remove(guid);
        }
    }
}