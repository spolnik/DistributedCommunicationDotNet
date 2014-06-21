using System;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.Transport.NetMQ
{
    public sealed class NmqServiceFactory : IServiceFactory
    {
        public IServer GetServer(IMessageReceiver messageReceiver, int port = -1)
        {
            return new NmqMessageServer(messageReceiver, port);
        }

        public IRequestSender GetRequestSender(Uri serviceUri)
        {
            return new NmqRequestSender(serviceUri);
        }
    }
}