using NProg.Distributed.Common.Service;
using NProg.Distributed.Common.Service.Messaging;
using Zyan.Communication;

namespace NProg.Distributed.Transport.Zyan
{
    internal sealed class ZyanMessageServer : IServer
    {
        private readonly MessageReceiver receiver;
        private ZyanComponentHost host;

        internal ZyanMessageServer(IMessageReceiver messageReceiver, int port)
        {
            receiver = (MessageReceiver)messageReceiver;
            host = new ZyanComponentHost("OrderService", port);
        }

        public void Run()
        {
            host.RegisterComponent<IMessageReceiver, MessageReceiver>(receiver);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && host != null)
            {
                host.Dispose();
                host = null;
            }
        }
    }
}