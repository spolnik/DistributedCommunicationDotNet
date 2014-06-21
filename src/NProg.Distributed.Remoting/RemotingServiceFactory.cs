using System;
using NProg.Distributed.Common.Service;
using NProg.Distributed.Common.Service.Messaging;

namespace NProg.Distributed.Remoting
{
    public sealed class RemotingServiceFactory : IServiceFactory
    {
        public IServer GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port)
        {
            return new RemotingOrderServer(port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return null;
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new RemotingRequestSender(serviceUri);
        }
    }

}