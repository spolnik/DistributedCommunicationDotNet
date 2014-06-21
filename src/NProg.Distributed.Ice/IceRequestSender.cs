using System;
using Ice;
using NProg.Distributed.Core.Service.Extensions;
using NProg.Distributed.Core.Service.Messaging;
using NProgDistributed.TheIce;

namespace NProg.Distributed.Transport.Ice
{
    internal sealed class IceRequestSender : RequestSenderBase
    {
        private readonly IMessageMapper messageMapper;
        private Communicator communicator;
        private readonly IMessageServicePrx proxy;

        internal IceRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            this.messageMapper = messageMapper;
            var address = string.Format("OrderService:tcp -p {1} -h {0}", serviceUri.Host, serviceUri.Port);

            communicator = Util.initialize();
            proxy = IMessageServicePrxHelper.checkedCast(communicator.stringToProxy(address));
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