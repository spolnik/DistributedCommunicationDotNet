using System;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.Remoting
{
    public sealed class RemotingServiceFactory : IServiceFactory
    {
        public IServer GetServer(IMessageReceiver messageReceiver, int port)
        {
            return new RemotingOrderServer(port);
        }

        public IMessageSender GetRequestSender(Uri serviceUri)
        {
            return new RemotingMessageSender(serviceUri);
        }
    }
}