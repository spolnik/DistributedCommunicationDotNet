﻿using System;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.Transport.WCF
{
    public sealed class WcfServiceFactory : IServiceFactory
    {
        public IServer GetServer(IMessageReceiver messageReceiver, int port)
        {
            return new WcfMessageServer(messageReceiver, port);
        }

        public IMessageSender GetRequestSender(Uri serviceUri)
        {
            return new WcfMessageSender(serviceUri);
        }
    }
}