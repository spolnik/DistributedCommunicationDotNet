using System;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.Transport.Thrift
{
    public sealed class ThriftServiceFactory : IServiceFactory
    {
        public IServer GetServer(IMessageReceiver messageReceiver, int port)
        {
            return new ThriftMessageServer(messageReceiver, port);
        }

        public IRequestSender GetRequestSender(Uri serviceUri)
        {
            return new ThriftRequestSender(serviceUri);
        }
    }
}