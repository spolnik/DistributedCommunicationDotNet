using System;
using System.Threading.Tasks;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.Thrift;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;

namespace NProg.Distributed.Transport.Thrift
{
    internal sealed class ThriftMessageServer : IServer
    {
        private readonly int port;
        private readonly MessageService.Iface receiver;
        private TThreadPoolServer server;

        internal ThriftMessageServer(IMessageReceiver messageReceiver, int port)
        {
            this.port = port;
            receiver = new ThriftMessageDispatcher(messageReceiver);
        }

        public void Run()
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && server != null)
            {
                server.Stop();
                server = null;
            }
        }
    }
}