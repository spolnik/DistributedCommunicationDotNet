using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.WCF
{
    public class WcfOrderServiceFactory : IServiceFactory<Guid, Order>
    {
        public IHandler<Guid, Order> GetHandler()
        {
            return null;
        }

        public IServer GetServer(IHandler<Guid, Order> handler, int port = -1)
        {
            return new WcfOrderServer(port);
        }

        public IHandler<Guid, Order> GetClient(Uri serviceUri)
        {
            return new WcfOrderClient(serviceUri);
        }
    }
}