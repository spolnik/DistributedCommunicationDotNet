using NProg.Distributed.Core.Service.Extensions;
using NProg.Distributed.Core.Service.Messaging;
using NProgDistributed.TheIce;

namespace NProg.Distributed.Transport.Ice
{
    internal static class MessageMapper
    {
        internal static Message Map(object message)
        {
            var messageDto = message.As<MessageDto>();

            return new Message
                {
                    Body = messageDto.body.ReadFromJson(messageDto.messageType)
                };
        }

        internal static object Map(Message order)
        {
            return new MessageDto
                {
                    body = order.Body.ToJsonString(),
                    messageType = order.MessageType
                };
        }
    }
}