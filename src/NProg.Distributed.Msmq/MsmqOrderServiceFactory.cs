using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;

namespace NProg.Distributed.Msmq
{
    public class MsmqOrderServiceFactory : IServiceFactory<Order>
    {
        public IHandler<Order> GetHandler()
        {
            return new SimpleOrderHandler("order_msmq.ndb");
        }

        public IServer GetServer(IHandler<Order> handler, int port = -1)
        {
            return new MsmqOrderServer(handler);
        }

        public IHandler<Order> GetClient(Uri serviceUri)
        {
            return new MsmqOrderClient();
        }
    }
}