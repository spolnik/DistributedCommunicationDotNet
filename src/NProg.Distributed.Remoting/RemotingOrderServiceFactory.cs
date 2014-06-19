using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Domain.Api;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Remoting
{
    public class RemotingOrderServiceFactory : IOrderServiceFactory<Guid, Order>
    {
        public IHandler<Guid, Order> GetHandler(IMessageMapper messageMapper)
        {
            return null;
        }

        public IServer GetServer(IHandler<Guid, Order> handler, int port = -1)
        {
            return new RemotingOrderServer(port);
        }

        public IOrderApi GetClient(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new RemotingOrderClient(serviceUri);
        }

        public IMessageMapper GetMessageMapper()
        {
            return new SimpleMessageMapper();
        }
    }

}