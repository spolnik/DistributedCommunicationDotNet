using System;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.Transport.ZeroMQ
{
    public sealed class ZmqServiceFactory : IServiceFactory
    {
        public IServer GetServer(IMessageReceiver messageReceiver, int port = -1)
        {
            return new ZmqMessageServer(messageReceiver, port);
        }

        public IMessageSender GetRequestSender(Uri serviceUri)
        {
            return new ZmqMessageSender(serviceUri);
        }
    }
}