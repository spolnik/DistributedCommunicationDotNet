using System;
using System.Threading.Tasks;
using Ice;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Ice
{
    public class IceMessageServer : IServer
    {
        private readonly IceMessageDispatcher handler;
        private readonly int port;
        private readonly Communicator communicator;

        public IceMessageServer(IMessageReceiver messagerReceiver, IMessageMapper messageMapper, int port)
        {
            this.handler = new IceMessageDispatcher(messagerReceiver, messageMapper);
            this.port = port;
            communicator = Util.initialize();
        }

        public void Start()
        {
            var adapter = communicator.createObjectAdapterWithEndpoints("OrderService", "tcp -p " + port);
            adapter.add(handler, communicator.stringToIdentity("OrderService"));
            adapter.activate();
            Task.Factory.StartNew(() => communicator.waitForShutdown());
        }

        public void Stop()
        {
            communicator.destroy();
        }
    }
}