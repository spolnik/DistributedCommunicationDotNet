using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.Zyan
{
    public class ZyanOrderServiceFactory : IServiceFactory<Order>
    {

        public IHandler<Order> GetHandler()
        {
            return new ZyanOrderHandler();
        }

        public IServer GetServer(IHandler<Order> handler, int port = -1)
        {
            return new ZyanOrderServer(port);
        }

        public IHandler<Order> GetClient(Uri serviceUri)
        {
            return new ZyanOrderClient(serviceUri);
        }
    }

}