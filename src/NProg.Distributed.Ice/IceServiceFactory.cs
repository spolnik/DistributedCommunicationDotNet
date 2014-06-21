using System;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.Transport.Ice
{
    public sealed class IceServiceFactory : IServiceFactory
    {
        public IServer GetServer(IMessageReceiver messageReceiver, int port)
        {
            return new IceMessageServer(messageReceiver, port);
        }

        public IRequestSender GetRequestSender(Uri serviceUri)
        {
            return new IceRequestSender(serviceUri);
        }
    }

}