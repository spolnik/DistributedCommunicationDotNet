using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;
using Zyan.Communication;

namespace NProg.Distributed.Zyan
{
    public class ZyanMessageServer : IRunnable
    {
        private readonly MessageReceiver receiver;
        private ZyanComponentHost host;

        public ZyanMessageServer(IMessageReceiver messageReceiver, int port)
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