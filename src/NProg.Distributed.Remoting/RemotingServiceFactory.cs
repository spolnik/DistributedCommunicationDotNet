using System;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Remoting
{
    public class RemotingServiceFactory : IServiceFactory
    {
        public IServer GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port)
        {
            return new RemotingOrderServer(port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return new SimpleMessageMapper();
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new RemotingRequestSender(serviceUri);
        }
    }

}