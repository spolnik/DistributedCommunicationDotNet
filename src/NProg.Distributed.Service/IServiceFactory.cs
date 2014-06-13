using System;

namespace NProg.Distributed.Service
{
    public interface IServiceFactory<TItem> where TItem : class
    {
        IHandler<TItem> GetHandler();

        IServer GetServer(IHandler<TItem> handler, int port = -1);

        IHandler<TItem> GetClient(Uri serviceUri);
    }

}