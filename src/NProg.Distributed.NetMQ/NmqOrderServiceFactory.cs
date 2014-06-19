using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Domain.Api;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.NetMQ
{
    public class NmqOrderServiceFactory : IOrderServiceFactory<Guid, Order>
    {
        public IHandler<Guid, Order> GetHandler(IMessageMapper messageMapper)
        {
            return new SimpleHandler<Guid, Order>(new OrderDaoFactory(), "order_netmq.ndb", messageMapper);
        }

        public IServer GetServer(IHandler<Guid, Order> handler, int port = -1)
        {
            return new NmqOrderServer(handler, port);
        }

        public IOrderApi GetClient(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new NmqOrderClient(serviceUri);
        }

        public IMessageMapper GetMessageMapper()
        {
            return null;
        }
    }
}