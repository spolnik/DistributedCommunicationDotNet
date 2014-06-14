using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;

namespace NProg.Distributed.NetMQ
{
    public class NmqOrderServiceFactory : IServiceFactory<Order>
    {

        public IHandler<Order> GetHandler()
        {
            return new SimpleOrderHandler("order_netmq.ndb");
        }

        public IServer GetServer(IHandler<Order> handler, int port = -1)
        {
            return new NmqOrderServer(handler);
        }

        public IHandler<Order> GetClient(Uri serviceUri)
        {
            return new NmqOrderClient();
        }
    }
}