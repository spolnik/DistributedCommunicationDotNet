using System;
using NProg.Distributed.Common.Service;
using NProg.Distributed.Common.Service.Messaging;

namespace NProg.Distributed.Transport.WCF
{
    public sealed class WcfServiceFactory : IServiceFactory
    {
        public IServer GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port)
        {
            return new WcfMessageServer(messageReceiver, port);
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