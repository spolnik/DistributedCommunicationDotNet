using System;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;

namespace NProg.Distributed.Thrift
{
    public class ThriftOrderServiceFactory : IServiceFactory<Guid, Domain.Order>
    {
        public IHandler<Guid, Domain.Order> GetHandler()
        {
            return new ThriftOrderHandler(new OrderDaoFactory(), "order_thrift.ndb");
        }

        public IServer GetServer(IHandler<Guid, Domain.Order> handler, int port)
        {
            return new ThriftOrderServer(handler, port);
        }

        public IHandler<Guid, Domain.Order> GetClient(Uri serviceUri)
        {
            return new ThriftOrderClient(serviceUri);
        }
    }
}