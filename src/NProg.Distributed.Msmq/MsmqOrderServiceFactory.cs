using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.Msmq
{
    public class MsmqOrderServiceFactory : IServiceFactory<Order>
    {
        public IHandler<Order> GetHandler()
        {
            return new MsmqOrderHandler();
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