using System.Threading.Tasks;
using Ice;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Ice
{
    public class IceMessageServer : IRunnable
    {
        private readonly IceMessageDispatcher receiver;
        private readonly int port;
        private Communicator communicator;

        public IceMessageServer(IMessageReceiver messagerReceiver, IMessageMapper messageMapper, int port)
        {
            receiver = new IceMessageDispatcher(messagerReceiver, messageMapper);
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