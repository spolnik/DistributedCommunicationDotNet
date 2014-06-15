using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;

namespace NProg.Distributed.ZeroMQ
{
    public class ZmqOrderServiceFactory : IServiceFactory<Order>
    {
        public IHandler<Order> GetHandler()
        {
            return new SimpleOrderHandler(new OrderDaoFactory(), "order_zeromq.ndb");
        }

        public IServer GetServer(IHandler<Order> handler, int port = -1)
        {
            return new ZmqOrderServer(handler, port);
        }

        public IHandler<Order> GetClient(Uri serviceUri)
        {
            return new ZmqOrderClient(serviceUri);
        }
    }
}