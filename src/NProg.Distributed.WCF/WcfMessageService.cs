using System.ServiceModel;
using NProg.Distributed.Service.Messaging;
using NProg.Distributed.WCF.Service;

namespace NProg.Distributed.WCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public sealed class WcfMessageService : IMessageService
    {
        private readonly IMessageReceiver messageReceiver;

        public WcfMessageService(IMessageReceiver receiver)
        {
            messageReceiver = receiver;
        }

        public Message Send(Message message)
        {
            return messageReceiver.Send(message);
        }
    }
}