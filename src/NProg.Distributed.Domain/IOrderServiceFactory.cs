using System;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Domain
{
    public interface IOrderServiceFactory<TKey, TValue> 
        where TValue : class
    {
        IHandler<TKey, TValue> GetHandler(IMessageMapper messageMapper);

        IServer GetServer(IHandler<TKey, TValue> handler, int port = -1);

        IMessageMapper GetMessageMapper();

        IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper);
    }
}