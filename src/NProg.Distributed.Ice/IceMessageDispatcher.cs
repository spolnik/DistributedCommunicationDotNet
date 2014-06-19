using Ice;
using NProg.Distributed.Service.Extensions;
using NProg.Distributed.Service.Messaging;
using NProgDistributed.TheIce;

namespace NProg.Distributed.Ice
{
    public class IceMessageDispatcher : MessageServiceDisp_
    {
        private readonly IMessageReceiver messageReceiver;
        private readonly IMessageMapper messageMapper;

        public IceMessageDispatcher(IMessageReceiver messageReceiver, IMessageMapper messageMapper)
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