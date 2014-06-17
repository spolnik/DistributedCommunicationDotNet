using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.Remoting
{
    public class RemotingOrderServiceFactory : IServiceFactory<Guid, Order>
    {
        public IHandler<Guid, Order> GetHandler()
        {
            return new RemotingOrderHandler();
        }

        public IServer GetServer(IHandler<Guid, Order> handler, int port = -1)
        {
            return new RemotingOrderServer(port);
        }

        public IHandler<Guid, Order> GetClient(Uri serviceUri)
        {
            return new RemotingOrderClient(serviceUri);
        }
    }

}