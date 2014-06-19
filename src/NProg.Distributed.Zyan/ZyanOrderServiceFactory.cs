using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Zyan
{
    public class ZyanOrderServiceFactory : IOrderServiceFactory<Guid, Order>
    {
        public IHandler<Guid, Order> GetHandler(IMessageMapper messageMapper)
        {
            return new ZyanOrderHandler(new OrderDaoFactory(), "order_zyan.ndb");
        }

        public IServer GetServer(IHandler<Guid, Order> handler, int port = -1)
        {
            return new ZyanOrderServer(handler, port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return null;
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new ZyanRequestSender(serviceUri);
        }

    }

}