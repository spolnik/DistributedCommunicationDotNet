using System;
using System.ServiceModel;
using NProg.Distributed.Service.Messaging;
using NProg.Distributed.WCF.Service;

namespace NProg.Distributed.WCF
{
    internal sealed class WcfRequestSender : RequestSenderBase
    {
        private ChannelFactory<IMessageService> channelFactory;
        private readonly IMessageService proxy;

        internal WcfRequestSender(Uri serviceUri)
        {
            var tcpBinding = new NetTcpBinding();
            var endpoint = new EndpointAddress(string.Format("net.tcp://{0}:{1}/OrderService", serviceUri.Host, serviceUri.Port));
            channelFactory = new ChannelFactory<IMessageService>(tcpBinding, endpoint);
            proxy = channelFactory.CreateChannel();
        }

        protected override Message SendInternal(Message message)
        {
            return proxy.Send(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && proxy != null)
            {
                channelFactory.Close();
                channelFactory = null;
            }
        }
    }
}