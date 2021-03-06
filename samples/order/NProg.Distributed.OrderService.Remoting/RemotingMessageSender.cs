﻿using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.OrderService.Remoting
{
    internal sealed class RemotingMessageSender : MessageSenderBase
    {
        private readonly RemotingOrderHandler proxy;

        internal RemotingMessageSender(Uri serviceUri)
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