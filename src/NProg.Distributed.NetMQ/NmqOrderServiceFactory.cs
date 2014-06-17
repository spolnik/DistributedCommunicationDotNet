using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;

namespace NProg.Distributed.NetMQ
{
    public class NmqOrderServiceFactory : IServiceFactory<Guid, Order>
    {

        public IHandler<Guid, Order> GetHandler()
        {
            return new SimpleHandler<Guid, Order>(new OrderDaoFactory(), "order_netmq.ndb");
        }

        public IServer GetServer(IHandler<Guid, Order> handler, int port = -1)
        {
            return new NmqOrderServer(handler, port);
        }

        public IHandler<Guid, Order> GetClient(Uri serviceUri)
        {
            return new NmqOrderClient(serviceUri);
        }
    }
}