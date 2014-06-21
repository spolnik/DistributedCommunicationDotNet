using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using NProg.Distributed.Core.Service;

namespace NProg.Distributed.Remoting
{
    internal sealed class RemotingOrderServer : IServer
    {
        private readonly int port;

        internal RemotingOrderServer(int port)
        {
            this.port = port;
        }

        public void Run()
        {
            var channel = new TcpChannel(port);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemotingOrderHandler), "OrderHandler", WellKnownObjectMode.Singleton);
        }

        public void Dispose()
        {
        }
    }
}