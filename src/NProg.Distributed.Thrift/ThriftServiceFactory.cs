using System;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.Transport.Thrift
{
    public sealed class ThriftServiceFactory : IServiceFactory
    {
        public IServer GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port)
        {
            return new ThriftMessageServer(messageReceiver, messageMapper, port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return new ThriftMessageMapper();
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new ThriftRequestSender(serviceUri, messageMapper);
        }
    }
}