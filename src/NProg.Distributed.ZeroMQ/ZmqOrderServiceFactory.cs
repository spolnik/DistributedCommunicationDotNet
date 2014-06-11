using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.ZeroMQ
{
    public class ZmqOrderServiceFactory : IOrderServiceFactory
    {

        public IHandler<Order> GetHandler()
        {
            return new ZmqOrderHandler();
        }

        public IServer GetServer(IHandler<Order> handler, int port = -1, string eventName = null)
        {
            return new ZmqOrderServer(handler, eventName);
        }

        public IHandler<Order> GetClient(Uri serviceUri)
        {
            return new ZmqOrderClient(serviceUri);
        }
    }
}