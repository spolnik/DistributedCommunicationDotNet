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
        private readonly MessageService.Iface handler;
        private TThreadPoolServer server;

        public ThriftOrderServer(IHandler<Guid, Domain.Order> handler, int port)
        {
            this.port = port;
            this.handler = (MessageService.Iface)handler;
        }

        public void Start()
        {
            try
            {
                var processor = new MessageService.Processor(handler);
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