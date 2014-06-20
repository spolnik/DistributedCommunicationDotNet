using System;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Ice
{
    public class IceServiceFactory : IServiceFactory
    {
        public IServer GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port)
        {
            return new IceMessageServer(messageReceiver, messageMapper, port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return new IceMessageMapper();
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new IceRequestSender(serviceUri, messageMapper);
        }
    }

}