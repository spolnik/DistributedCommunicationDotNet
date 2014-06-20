using System;
using System.Collections.Generic;
using NProg.Distributed.NDatabase;
using NProg.Distributed.OrderService.Handlers;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Remoting
{
    public sealed class RemotingOrderHandler : MarshalByRefObject
    {
        private readonly MessageReceiver messageReceiver;

        public RemotingOrderHandler()
        {
            var orderDaoFactory = new OrderDaoFactory();
            var register = new List<IMessageHandler>
            {
                new AddOrderHandler(orderDaoFactory),
                new GetOrderHandler(orderDaoFactory),
                new RemoveOrderHandler(orderDaoFactory)
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