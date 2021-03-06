﻿using System;
using NProg.Distributed.Core.Service.Messaging;
using Zyan.Communication;

namespace NProg.Distributed.Transport.Zyan
{
    internal sealed class ZyanMessageSender : MessageSenderBase
    {
        private readonly IMessageReceiver proxy;
        private ZyanConnection connection;

        internal ZyanMessageSender(Uri serviceUri)
        {
            var serverUrl = string.Format("tcp://{0}:{1}/OrderService", serviceUri.Host, serviceUri.Port);
            connection = new ZyanConnection(serverUrl);
            proxy = connection.CreateProxy<IMessageReceiver>();
        }

        protected override Message SendInternal(Message message)
        {
            return proxy.Send(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && connection != null)
            {
                connection.Dispose();
                connection = null;
            }
        }
    }
}