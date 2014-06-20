using System;
using System.Collections.Generic;
using NProg.Distributed.OrderService.Handlers;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Remoting
{
    public sealed class RemotingOrderHandler : MarshalByRefObject
    {
        private readonly MessageReceiver messageReceiver;

        public RemotingOrderHandler()
        {
            var register = new List<IMessageHandler>
            {
                new AddOrderHandler(),
                new GetOrderHandler(),
                new RemoveOrderHandler()
            };

            var handlerRegister = new HandlerRegister(register);
            messageReceiver = new MessageReceiver(handlerRegister);
        }

        public Message Send(Message message)
        {
            return messageReceiver.Send(message);
        }
    }
}