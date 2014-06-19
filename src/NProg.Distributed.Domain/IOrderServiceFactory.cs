using System;
using NProg.Distributed.Domain.Api;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Domain
{
    public interface IOrderServiceFactory<TKey, TValue> 
        where TValue : class
    {
        IHandler<TKey, TValue> GetHandler(IMessageMapper messageMapper);

        IServer GetServer(IHandler<TKey, TValue> handler, int port = -1);

        IOrderApi GetClient(Uri serviceUri, IMessageMapper messageMapper);

        IMessageMapper GetMessageMapper();
    }
}