using NProg.Distributed.Domain;
using NProg.Distributed.Service;
using Zyan.Communication;

namespace NProg.Distributed.Zyan
{
    public class ZyanOrderServer : IServer
    {
        private readonly ZyanComponentHost host;

        public ZyanOrderServer(int port)
        {
            host = new ZyanComponentHost("OrderService", port);
        }

        public void Start()
        {
            host.RegisterComponent<IHandler<Order>, ZyanOrderHandler>(ActivationType.Singleton);
        }

        public void Stop()
        {
            host.Dispose();
        }
    }
}