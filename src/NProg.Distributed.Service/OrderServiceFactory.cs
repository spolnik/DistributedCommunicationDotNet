using System;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Service
{
    public interface IServiceFactory
    {
        IServer GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port);

        IMessageMapper GetMessageMapper();

        IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper);
    }
}