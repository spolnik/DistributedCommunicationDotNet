using System;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.ZeroMQ
{
    public class ZmqServiceFactory : IServiceFactory
    {
        public IMessageReceiver GetMessageReceiver(IHandlerRegister handlerRegister)
        {
            return new MessageReceiver(handlerRegister);
        }

        public IServer GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port = -1)
        {
            return new ZmqOrderServer(messageReceiver, port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return null;
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new ZmqRequestSender(serviceUri);
        }
    }
}