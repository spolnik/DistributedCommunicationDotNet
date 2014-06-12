using System;
using NProg.Distributed.Service;

namespace NProg.Distributed.Thrift
{
    public class ThriftOrderServiceFactory : IOrderServiceFactory
    {
        public IHandler<Domain.Order> GetHandler()
        {
            return new ThriftOrderHandler();
        }

        public IServer GetServer(IHandler<Domain.Order> handler, int port)
        {
            return new ThriftOrderServer(handler, port);
        }

        public IHandler<Domain.Order> GetClient(Uri serviceUri)
        {
            return new ThriftOrderClient(serviceUri);
        }
    }
}