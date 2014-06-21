using Ice;
using NProg.Distributed.Core.Service.Extensions;
using NProg.Distributed.Core.Service.Messaging;
using NProgDistributed.TheIce;

namespace NProg.Distributed.Transport.Ice
{
    internal sealed class IceMessageDispatcher : IMessageServiceDisp_
    {
        private readonly IMessageReceiver messageReceiver;
        private readonly IMessageMapper messageMapper;

        internal IceMessageDispatcher(IMessageReceiver messageReceiver, IMessageMapper messageMapper)
        {
            this.messageReceiver = messageReceiver;
            this.messageMapper = messageMapper;
        }

        public override MessageDto Send(MessageDto messageDto, Current current)
        {
            var message = messageMapper.Map(messageDto);
            var response = messageReceiver.Send(message);

            return messageMapper.Map(response).As<MessageDto>();
        }
    }
}