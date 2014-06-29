using System;
using System.ServiceModel;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.Transport.WCF.Service;

namespace NProg.Distributed.Transport.WCF
{
    internal sealed class WcfMessageServer : IServer
    {
        private readonly IMessageReceiver messageReceiver;
        private readonly int port;
        private ServiceHost host;

        internal WcfMessageServer(IMessageReceiver messageReceiver, int port)
        {
            this.messageReceiver = messageReceiver;
            this.port = port;
        }

        public void Run()
        {
            var serverUri = new Uri(string.Format("net.tcp://localhost:{0}/OrderService", port));
            var wcfMessageService = new WcfMessageService(messageReceiver);

            host = new ServiceHost(wcfMessageService, serverUri);
            host.AddServiceEndpoint(typeof (IMessageService), new NetTcpBinding(), "");
            host.Open();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && host != null)
            {
                host.Close();
                host = null;
            }
        }
    }
}