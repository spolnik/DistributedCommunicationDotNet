using System;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Service
{
    public interface IServiceFactory<TKey, TValue> 
        where TValue : class
    {
        IHandler<TKey, TValue> GetHandler(IMessageMapper messageMapper);

        IServer GetServer(IHandler<TKey, TValue> handler, int port = -1);

        IHandler<TKey, TValue> GetClient(Uri serviceUri, IMessageMapper messageMapper);

        IMessageMapper GetMessageMapper();
    }
}