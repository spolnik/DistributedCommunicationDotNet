using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;
using Zyan.Communication;

namespace NProg.Distributed.Zyan
{
    public class ZyanOrderServer : IServer
    {
        private readonly ZyanOrderHandler handler;
        private readonly ZyanComponentHost host;

        public ZyanOrderServer(IHandler<Guid, Order> handler, int port)
        {
            this.handler = (ZyanOrderHandler)handler;
            host = new ZyanComponentHost("OrderService", port);
        }

        public void Start()
        {
            host.RegisterComponent<IMessageHandler, ZyanOrderHandler>(handler);
        }

        public void Stop()
        {
            host.Dispose();
        }
    }
}