using NProg.Distributed.Service.Extensions;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Thrift
{
    public class ThriftMessageDispatcher : MessageService.Iface
    {
        private readonly IMessageReceiver messageReceiver;
        private readonly IMessageMapper messageMapper;

        public ThriftMessageDispatcher(IMessageReceiver messageReceiver, IMessageMapper messageMapper)
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