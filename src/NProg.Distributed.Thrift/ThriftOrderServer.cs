using System;
using System.Threading.Tasks;
using NProg.Distributed.Service;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;

namespace NProg.Distributed.Thrift
{
    public class ThriftOrderServer : IServer
    {
        private readonly int port;
        private readonly OrderService.Iface handler;
        private TThreadPoolServer server;

        public ThriftOrderServer(IHandler<Domain.Order> handler, int port)
        {
            this.port = port;
            this.handler = (OrderService.Iface)handler;
        }

        public void Start()
        {
            try
            {
                var processor = new OrderService.Processor(handler);
                var serverTransport = new TServerSocket(port);
                server = new TThreadPoolServer(processor, serverTransport, new TTransportFactory(), new TCompactProtocol.Factory());

                Task.Factory.StartNew(() => server.Serve());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Stop()
        {
            server.Stop();
        }
    }
}