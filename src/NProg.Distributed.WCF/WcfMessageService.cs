using System.ServiceModel;
using NProg.Distributed.Service.Messaging;
using NProg.Distributed.WCF.Service;

namespace NProg.Distributed.WCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    internal sealed class WcfMessageService : IMessageService
    {
        private readonly IMessageReceiver messageReceiver;

        internal WcfMessageService(IMessageReceiver receiver)
        {
            messageReceiver = receiver;
        }

        public Message Send(Message message)
        {
            return messageReceiver.Send(message);
        }
    }
}