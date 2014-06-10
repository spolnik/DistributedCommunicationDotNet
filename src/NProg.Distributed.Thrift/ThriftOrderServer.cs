using System;
using NProg.Distributed.Service;
using Thrift.Server;
using Thrift.Transport;

namespace NProg.Distributed.Thrift
{
    public class ThriftOrderServer : IServer
    {
        private readonly int port;
        private readonly OrderService.Iface handler;
        private TSimpleServer server;

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
                server = new TSimpleServer(processor, serverTransport);

                // Use this for a multithreaded server
                // server = new TThreadPoolServer(processor, serverTransport);

                server.Serve();
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