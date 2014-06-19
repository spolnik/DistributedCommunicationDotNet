using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Thrift
{
    public class ThriftOrderServiceFactory : IOrderServiceFactory<Guid, Order>
    {
        public IHandler<Guid, Order> GetHandler(IMessageMapper messageMapper)
        {
            return new ThriftOrderHandler(new OrderDaoFactory(), "order_thrift.ndb", messageMapper);
        }

        public IServer GetServer(IHandler<Guid, Order> handler, int port)
        {
            return new ThriftOrderServer(handler, port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return new ThriftMessageMapper();
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new ThriftRequestSender(serviceUri, messageMapper);
        }
    }
}