using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.Remoting
{
    public class RemotingOrderServiceFactory : IServiceFactory<Order>
    {

        public IHandler<Order> GetHandler()
        {
            return new RemotingOrderHandler();
        }

        public IServer GetServer(IHandler<Order> handler, int port = -1)
        {
            return new RemotingOrderServer(port);
        }

        public IHandler<Order> GetClient(Uri serviceUri)
        {
            return new RemotingOrderClient(serviceUri);
        }
    }

}