using System;
using NProg.Distributed.Common.Service;
using NProg.Distributed.Common.Service.Messaging;

namespace NProg.Distributed.Transport.ZeroMQ
{
    public sealed class ZmqServiceFactory : IServiceFactory
    {
        public IServer GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port = -1)
        {
            return new ZmqMessageServer(messageReceiver, port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return null;
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new ZmqRequestSender(serviceUri);
        }
    }
}