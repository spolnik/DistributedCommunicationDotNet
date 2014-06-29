using NProg.Distributed.Core.Service.Extensions;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.Thrift;

namespace NProg.Distributed.Transport.Thrift
{
    internal sealed class ThriftMessageDispatcher : MessageService.Iface
    {
        private readonly IMessageReceiver messageReceiver;

        internal ThriftMessageDispatcher(IMessageReceiver messageReceiver)
        {
            this.messageReceiver = messageReceiver;
        }

        public ThriftMessage Send(ThriftMessage thriftMessage)
        {
            var message = MessageMapper.Map(thriftMessage);
            var response = messageReceiver.Send(message);

            return MessageMapper.Map(response).As<ThriftMessage>();
        }
    }
}