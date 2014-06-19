using System;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Thrift
{
    public class ThriftServiceFactory : IServiceFactory
    {
        public IServer GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port)
        {
            return new ThriftOrderServer(messageReceiver, port);
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