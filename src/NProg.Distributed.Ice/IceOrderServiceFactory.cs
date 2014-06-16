using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;

namespace NProg.Distributed.Ice
{
    public class IceOrderServiceFactory : IServiceFactory<Domain.Order>
    {
        public IHandler<Domain.Order> GetHandler()
        {
            return new IceOrderHandler(new OrderDaoFactory(), "order_ice.ndb");
        }

        public IServer GetServer(IHandler<Domain.Order> handler, int port = -1)
        {
            return new IceOrderServer(handler, port);
        }

        public IHandler<Domain.Order> GetClient(Uri serviceUri)
        {
            return new IceOrderClient(serviceUri);
        }
    }

}