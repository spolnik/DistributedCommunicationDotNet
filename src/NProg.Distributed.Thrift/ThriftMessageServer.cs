using System;
using System.Threading.Tasks;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;

namespace NProg.Distributed.Thrift
{
    public class ThriftMessageServer : IServer
    {
        private readonly int port;
        private readonly MessageService.Iface receiver;
        private TThreadPoolServer server;

        public ThriftMessageServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port)
        {
            this.port = port;
            receiver = new ThriftMessageDispatcher(messageReceiver, messageMapper);
        }

        public void Start()
        {
            try
            {
                var processor = new MessageService.Processor(receiver);
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