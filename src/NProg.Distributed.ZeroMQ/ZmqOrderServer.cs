using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Messaging.Spec;
using NProg.Distributed.Service;
using NProg.Distributed.ZeroMQ.Command;
using NProg.Distributed.ZeroMQ.Messaging;

namespace NProg.Distributed.ZeroMQ
{
    public class ZmqOrderServer : IServer
    {
        private readonly string eventName;
        private readonly IHandler<Order> handler;
        
        public ZmqOrderServer(IHandler<Order> handler, string eventName)
        {
            this.eventName = eventName;
            this.handler = handler;
        }

        public void Start()
        {
            switch (eventName)
            {
                case AddOrderCommand.Name:
                    StartListening(AddOrderCommand.Name, MessagePattern.FireAndForget);
                    break;
                case GetOrderCommand.Name:
                    StartListening(GetOrderCommand.Name, MessagePattern.RequestResponse);
                    break;
                case RemoveOrderCommand.Name:
                    StartListening(RemoveOrderCommand.Name, MessagePattern.RequestResponse);
                    break;
            }
        }

        public void Stop()
        {
        }

        private void StartListening(string name, MessagePattern pattern)
        {
            var queue = MessageQueueFactory.CreateInbound(name, pattern);
            Console.WriteLine("Listening on: {0}", queue.Address);
            queue.Listen(x =>
            {
                if (x.BodyType == typeof(AddOrderCommand))
                {
                    handler.Add(x.BodyAs<AddOrderCommand>().Order);
                }
                else if (x.BodyType == typeof(GetOrderCommand))
                {
                    GetOrder(x.BodyAs<GetOrderCommand>().OrderId, x, queue);
                }
                else if (x.BodyType == typeof(RemoveOrderCommand))
                {
                    RemoveOrder(x.BodyAs<RemoveOrderCommand>().OrderId, x, queue);
                }
            });
        }

        private void GetOrder(Guid orderId, Message message, IMessageQueue queue)
        {
            Console.WriteLine("Starting GetOrder for: {0}, at: {1}", orderId, DateTime.Now.TimeOfDay);

            var order = handler.Get(orderId);
            var responseQueue = queue.GetReplyQueue(message);

            responseQueue.Send(new Message
            {
                Body = order
            });

            Console.WriteLine("Returned: {0} for GetOrder, to: {1}, at: {2}", order.OrderId, responseQueue.Address,
                DateTime.Now.TimeOfDay);
        }

        private void RemoveOrder(Guid orderId, Message message, IMessageQueue queue)
        {
            Console.WriteLine("Starting RemoveOrder for: {0}, at: {1}", orderId, DateTime.Now.TimeOfDay);

            var status = handler.Remove(orderId);
            var responseQueue = queue.GetReplyQueue(message);

            responseQueue.Send(new Message
            {
                Body = status
            });

            Console.WriteLine("Returned: {0} for RemoveOrder, to: {1}, at: {2}", status, responseQueue.Address,
                DateTime.Now.TimeOfDay);
        }
    }
}