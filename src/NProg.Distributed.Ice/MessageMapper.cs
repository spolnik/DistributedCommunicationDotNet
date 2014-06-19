using NProg.Distributed.Messaging;
using NProg.Distributed.Messaging.Extensions;
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