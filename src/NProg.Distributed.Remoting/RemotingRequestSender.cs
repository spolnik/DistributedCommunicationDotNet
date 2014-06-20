using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Remoting
{
    internal sealed class RemotingRequestSender : RequestSenderBase
    {
        private readonly RemotingOrderHandler proxy;

        internal RemotingRequestSender(Uri serviceUri)
        {
            var tcpChannel = new TcpChannel();
            ChannelServices.RegisterChannel(tcpChannel, false);
            
            var address = string.Format("tcp://{0}:{1}/OrderHandler", serviceUri.Host, serviceUri.Port);
            proxy = (RemotingOrderHandler)Activator.GetObject(typeof(RemotingOrderHandler), address);
        }

        protected override Message SendInternal(Message message)
        {
            return proxy.Send(message);
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}