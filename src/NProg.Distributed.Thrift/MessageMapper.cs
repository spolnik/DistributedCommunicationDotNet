using NProg.Distributed.Service.Extensions;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Thrift
{
    internal static class MessageMapper
    {
        internal static Message Map(ThriftMessage message)
        {
            return new Message
            {
                Body = message.Body.ReadFromJson(message.MessageType)
            };
        }

        internal static ThriftMessage Map(Message order)
        {
            return new ThriftMessage
            {
                Body = order.Body.ToJsonString(),
                MessageType = order.MessageType
            };
        }
    }
}