using System;
using System.ServiceModel;
using NProg.Distributed.Service;
using NProg.Distributed.WCF.Service;

namespace NProg.Distributed.WCF
{
    public class WcfOrderServer : IServer
    {
        private readonly int port;
        private ServiceHost host;

        public WcfOrderServer(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            var serverUri = new Uri(string.Format("net.tcp://localhost:{0}/OrderService", port));
            host = new ServiceHost(typeof(WcfOrderHandler), serverUri);
            host.AddServiceEndpoint(typeof (IOrderService), new NetTcpBinding(), "");
            host.Open();
        }

        public void Stop()
        {
            host.Close();
        }
    }
}