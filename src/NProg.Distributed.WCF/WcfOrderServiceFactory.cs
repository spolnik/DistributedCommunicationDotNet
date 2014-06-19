using System;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.WCF
{
    public class WcfServiceFactory : IServiceFactory
    {
        public IServer GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port)
        {
            return new WcfOrderServer(port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return null;
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new WcfRequestSender(serviceUri);
        }
    }
}