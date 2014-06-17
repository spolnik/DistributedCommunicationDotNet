using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;

namespace NProg.Distributed.ZeroMQ
{
    public class ZmqOrderServiceFactory : IServiceFactory<Guid, Order>
    {
        public IHandler<Guid, Order> GetHandler()
        {
            return new SimpleHandler<Guid, Order>(new OrderDaoFactory(), "order_zeromq.ndb");
        }

        public IServer GetServer(IHandler<Guid, Order> handler, int port = -1)
        {
            return new ZmqOrderServer(handler, port);
        }

        public IHandler<Guid, Order> GetClient(Uri serviceUri)
        {
            return new ZmqOrderClient(serviceUri);
        }
    }
}