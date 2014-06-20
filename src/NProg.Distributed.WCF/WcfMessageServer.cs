using System;
using System.ServiceModel;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;
using NProg.Distributed.WCF.Service;

namespace NProg.Distributed.WCF
{
    public sealed class WcfMessageServer : IRunnable
    {
        private readonly IMessageReceiver messageReceiver;
        private readonly int port;
        private ServiceHost host;

        public WcfMessageServer(IMessageReceiver messageReceiver, int port)
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