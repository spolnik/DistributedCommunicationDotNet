using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.WCF
{
    public class WcfOrderServiceFactory : IOrderServiceFactory
    {
        public IHandler<Order> GetHandler()
        {
            return new WcfOrderHandler();
        }

        public IServer GetServer(IHandler<Order> handler, int port = -1)
        {
            return new WcfOrderServer(port);
        }

        public IHandler<Order> GetClient(Uri serviceUri)
        {
            return new WcfOrderClient(serviceUri);
        }
    }
}