using NProg.Distributed.Core.Service.Extensions;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.Thrift;

namespace NProg.Distributed.Transport.Thrift
{
    internal static class MessageMapper
    {
        public static Message Map(object message)
        {
            var thriftMessage = message.As<ThriftMessage>();

            return new Message
                {
                    Body = thriftMessage.Body.ReadFromJson(thriftMessage.MessageType)
                };
        }

        public static object Map(Message order)
        {
            return new ThriftMessage
                {
                    Body = order.Body.ToJsonString(),
                    MessageType = order.MessageType
                };
        }
    }
}