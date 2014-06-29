using System.Threading.Tasks;
using Ice;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.Transport.Ice
{
    internal sealed class IceMessageServer : IServer
    {
        private readonly IceMessageDispatcher receiver;
        private readonly int port;
        private Communicator communicator;

        internal IceMessageServer(IMessageReceiver messageReceiver, int port)
        {
            receiver = new IceMessageDispatcher(messageReceiver);
            this.port = port;
            communicator = Util.initialize();
        }

        public void Run()
        {
            var adapter = communicator.createObjectAdapterWithEndpoints("OrderService", "tcp -p " + port);
            adapter.add(receiver, communicator.stringToIdentity("OrderService"));
            adapter.activate();
            Task.Factory.StartNew(() => communicator.waitForShutdown());
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && communicator!= null)
            {
                communicator.destroy();
                communicator = null;
            }
        }
    }
}