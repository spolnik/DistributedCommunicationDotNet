﻿using System;
using Ice;
using NProg.Distributed.Service.Extensions;
using NProg.Distributed.Service.Messaging;
using NProgDistributed.TheIce;

namespace NProg.Distributed.Ice
{
    public class IceRequestSender : RequestSender
    {
        private readonly IMessageMapper messageMapper;
        private Communicator communicator;
        private readonly MessageServicePrx proxy;

        public IceRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            this.messageMapper = messageMapper;
            var address = string.Format("OrderService:tcp -p {1} -h {0}", serviceUri.Host, serviceUri.Port);

            communicator = Util.initialize();
            proxy = MessageServicePrxHelper.checkedCast(communicator.stringToProxy(address));
        }

        protected override Message SendInternal(Message message)
        {
            var response = proxy.Send(messageMapper.Map(message).As<MessageDto>());
            return messageMapper.Map(response);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && communicator != null)
            {
                communicator.destroy();
                communicator = null;
            }
        }
    }
}