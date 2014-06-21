using System;
using NProg.Distributed.Common.Service;
using NProg.Distributed.Common.Service.Messaging;

namespace NProg.Distributed.Transport.Zyan
{
    public sealed class ZyanServiceFactory : IServiceFactory
    {
        public IServer GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port = -1)
        {
            return new ZyanMessageServer(messageReceiver, port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return null;
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new ZyanRequestSender(serviceUri);
        }
    }
}