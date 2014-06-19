using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Ice
{
    public class IceOrderServiceFactory : IServiceFactory<Guid, Order>
    {
        public IHandler<Guid, Order> GetHandler(IMessageMapper messageMapper)
        {
            return new IceOrderHandler(new OrderDaoFactory(), "order_ice.ndb", messageMapper);
        }

        public IServer GetServer(IHandler<Guid, Order> handler, int port = -1)
        {
            return new IceOrderServer(handler, port);
        }

        public IHandler<Guid, Order> GetClient(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new IceOrderClient(serviceUri, messageMapper);
        }

        public IMessageMapper GetMessageMapper()
        {
            return new IceMessageMapper();
        }
    }

}