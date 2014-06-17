using System;

namespace NProg.Distributed.Service
{
    public interface IServiceFactory<TKey, TValue> where TValue : class
    {
        IHandler<TKey, TValue> GetHandler();

        IServer GetServer(IHandler<TKey, TValue> handler, int port = -1);

        IHandler<TKey, TValue> GetClient(Uri serviceUri);
    }
}