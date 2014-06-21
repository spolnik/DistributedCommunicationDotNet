using System;
using NProg.Distributed.Common.Service;
using NProg.Distributed.Common.Service.Messaging;

namespace NProg.Distributed.Transport.Ice
{
    public sealed class IceServiceFactory : IServiceFactory
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