using NProg.Distributed.Service.Extensions;
using NProg.Distributed.Service.Messaging;
using NProgDistributed.TheIce;

namespace NProg.Distributed.Ice
{
    internal static class MessageMapper
    {
        internal static Message Map(MessageDto message)
        {
            return new Message
            {
                Body = message.body.ReadFromJson(message.messageType)
            };
        }

        internal static MessageDto Map(Message order)
        {
            return new MessageDto
            {
                body = order.Body.ToJsonString(),
                messageType = order.MessageType
            };
        }
    }
}