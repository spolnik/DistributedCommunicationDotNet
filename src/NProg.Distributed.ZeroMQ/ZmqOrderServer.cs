using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.ZeroMQ
{
    public class ZmqOrderServer : IServer
    {
        private int port;
        private IHandler<Order> handler;

        public ZmqOrderServer(IHandler<Domain.Order> handler, int port)
        {
            this.port = port;
            this.handler = handler;
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}