using Ice;
using NProg.Distributed.Core.Service.Extensions;
using NProg.Distributed.Core.Service.Messaging;
using NProgDistributed.TheIce;

namespace NProg.Distributed.Transport.Ice
{
    internal sealed class IceMessageDispatcher : IMessageServiceDisp_
    {
        private readonly IMessageReceiver messageReceiver;

        internal IceMessageDispatcher(IMessageReceiver messageReceiver)
        {
            this.messageReceiver = messageReceiver;
        }

        public override MessageDto Send(MessageDto messageDto, Current current)
        {
            var message = MessageMapper.Map(messageDto);
            var response = messageReceiver.Send(message);

            return MessageMapper.Map(response).As<MessageDto>();
        }
    }

}