using System;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.Transport.Zyan
{
    public sealed class ZyanServiceFactory : IServiceFactory
    {
        public IServer GetServer(IMessageReceiver messageReceiver, int port = -1)
        {
            return new ZyanMessageServer(messageReceiver, port);
        }

        public IMessageSender GetRequestSender(Uri serviceUri)
        {
            return new ZyanMessageSender(serviceUri);
        }
    }
}