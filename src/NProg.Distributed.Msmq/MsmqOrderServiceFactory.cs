using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.Msmq
{
    public class MsmqOrderServiceFactory : IOrderServiceFactory
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