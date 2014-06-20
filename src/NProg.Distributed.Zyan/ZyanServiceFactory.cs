using System;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Zyan
{
    public class ZyanServiceFactory : IServiceFactory
    {
        public IMessageReceiver GetMessageReceiver(IHandlerRegister handlerRegister)
        {
            return new MessageReceiver(handlerRegister);
        }

        public IRunnable GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port = -1)
        {
            return new ZyanMessageServer(messageReceiver, port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return null;
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new ZyanRequestSender(serviceUri);
        }

    }

}