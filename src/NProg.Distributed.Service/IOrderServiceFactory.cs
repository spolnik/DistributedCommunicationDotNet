using System;
using NProg.Distributed.Domain;

namespace NProg.Distributed.Service
{
    public interface IOrderServiceFactory
    {
        IHandler<Order> GetHandler();

        IServer GetServer(IHandler<Order> handler, int port = -1, string eventName = null);

        IHandler<Order> GetClient(Uri serviceUri);
    }
}