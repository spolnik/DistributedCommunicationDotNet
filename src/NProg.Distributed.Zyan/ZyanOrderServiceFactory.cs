using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;

namespace NProg.Distributed.Zyan
{
    public class ZyanOrderServiceFactory : IServiceFactory<Guid, Order>
    {
        public IHandler<Guid, Order> GetHandler()
        {
            return new ZyanOrderHandler(new OrderDaoFactory(), "order_zyan.ndb");
        }

        public IServer GetServer(IHandler<Guid, Order> handler, int port = -1)
        {
            return new ZyanOrderServer(handler, port);
        }

        public IHandler<Guid, Order> GetClient(Uri serviceUri)
        {
            return new ZyanOrderClient(serviceUri);
        }
    }

}