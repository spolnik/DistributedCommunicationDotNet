using System;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;

namespace NProg.Distributed.Ice
{
    public class IceOrderServiceFactory : IServiceFactory<Guid, Domain.Order>
    {
        public IHandler<Guid, Domain.Order> GetHandler()
        {
            return new IceOrderHandler(new OrderDaoFactory(), "order_ice.ndb");
        }

        public IServer GetServer(IHandler<Guid, Domain.Order> handler, int port = -1)
        {
            return new IceOrderServer(handler, port);
        }

        public IHandler<Guid, Domain.Order> GetClient(Uri serviceUri)
        {
            return new IceOrderClient(serviceUri);
        }
    }

}