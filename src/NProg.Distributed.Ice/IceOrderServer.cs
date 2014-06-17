using System;
using System.Threading.Tasks;
using Ice;
using NProg.Distributed.Service;

namespace NProg.Distributed.Ice
{
    public class IceOrderServer : IServer
    {
        private readonly IceOrderHandler handler;
        private readonly int port;
        private readonly Communicator communicator;

        public IceOrderServer(IHandler<Guid, Domain.Order> handler, int port)
        {
            this.handler = (IceOrderHandler) handler;
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