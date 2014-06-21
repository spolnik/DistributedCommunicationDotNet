using NProg.Distributed.Core.Service.Extensions;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.Thrift;

namespace NProg.Distributed.Transport.Thrift
{
    internal sealed class ThriftMessageDispatcher : MessageService.Iface
    {
        private readonly IMessageReceiver messageReceiver;
        private readonly IMessageMapper messageMapper;

        internal ThriftMessageDispatcher(IMessageReceiver messageReceiver, IMessageMapper messageMapper)
        {
            this.messageReceiver = messageReceiver;
            this.messageMapper = messageMapper;
        }

        public ThriftMessage Send(ThriftMessage thriftMessage)
        {
            var message = messageMapper.Map(thriftMessage);
            var response = messageReceiver.Send(message);

            return messageMapper.Map(response).As<ThriftMessage>();
        }
    }
}