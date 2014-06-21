using NProg.Distributed.Common.Service.Extensions;
using NProg.Distributed.Common.Service.Messaging;
using NProg.Distributed.Thrift;

namespace NProg.Distributed.Transport.Thrift
{
    internal sealed class ThriftMessageMapper : IMessageMapper
    {
        public Message Map(object message)
        {
            var thriftMessage = message.As<ThriftMessage>();

            return new Message
            {
                Body = thriftMessage.Body.ReadFromJson(thriftMessage.MessageType)
            };
        }

        public object Map(Message order)
        {
            return new ThriftMessage
            {
                Body = order.Body.ToJsonString(),
                MessageType = order.MessageType
            };
        }
    }
}