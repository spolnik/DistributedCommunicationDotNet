using System;
using NProg.Distributed.Domain;
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

        public IMessageMapper GetMessageMapper()
        {
            return null;
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new WcfRequestSender(serviceUri);
        }
    }
}