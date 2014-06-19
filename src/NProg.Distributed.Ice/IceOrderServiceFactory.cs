using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Ice
{
    public class IceOrderServiceFactory : IOrderServiceFactory<Guid, Order>
    {
        public IHandler<Guid, Order> GetHandler(IMessageMapper messageMapper)
        {
            return new IceOrderHandler(new OrderDaoFactory(), "order_ice.ndb", messageMapper);
        }

        public IServer GetServer(IHandler<Guid, Order> handler, int port = -1)
        {
            return new IceOrderServer(handler, port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return new IceMessageMapper();
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new IceRequestSender(serviceUri, messageMapper);
        }
    }

}