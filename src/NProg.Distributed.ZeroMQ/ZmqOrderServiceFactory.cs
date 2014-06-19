﻿using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.ZeroMQ
{
    public class ZmqOrderServiceFactory : IOrderServiceFactory<Guid, Order>
    {
        public IHandler<Guid, Order> GetHandler(IMessageMapper messageMapper)
        {
            return new SimpleHandler<Guid, Order>(new OrderDaoFactory(), "order_zeromq.ndb", messageMapper);
        }

        public IServer GetServer(IHandler<Guid, Order> handler, int port = -1)
        {
            return new ZmqOrderServer(handler, port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return null;
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new ZmqRequestSender(serviceUri);
        }
    }
}