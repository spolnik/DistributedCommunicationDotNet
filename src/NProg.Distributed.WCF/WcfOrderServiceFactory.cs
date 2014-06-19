using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Domain.Api;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.WCF
{
    public class WcfOrderServiceFactory : IOrderServiceFactory<Guid, Order>
    {
        public IHandler<Guid, Order> GetHandler(IMessageMapper messageMapper)
        {
            return null;
        }

        public IServer GetServer(IHandler<Guid, Order> handler, int port = -1)
        {
            return new WcfOrderServer(port);
        }

        public IOrderApi GetClient(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new WcfOrderClient(serviceUri);
        }

        public IMessageMapper GetMessageMapper()
        {
            return null;
        }
    }
}