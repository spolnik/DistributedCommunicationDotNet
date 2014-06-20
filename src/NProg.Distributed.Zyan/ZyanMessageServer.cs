using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;
using Zyan.Communication;

namespace NProg.Distributed.Zyan
{
    public class ZyanMessageServer : IServer
    {
        private readonly MessageReceiver receiver;
        private readonly ZyanComponentHost host;

        public ZyanMessageServer(IMessageReceiver messageReceiver, int port)
        {
            receiver = (MessageReceiver)messageReceiver;
            host = new ZyanComponentHost("OrderService", port);
        }

        public void Start()
        {
            host.RegisterComponent<IMessageReceiver, MessageReceiver>(receiver);
        }

        public void Stop()
        {
            host.Dispose();
        }
    }
}