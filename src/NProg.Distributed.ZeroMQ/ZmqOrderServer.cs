using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private Task addOrderTask;
        private Task getOrderTask;
        private Task removeOrderTask;

        public ZmqOrderServer(IHandler<Order> handler, string eventName)
        {
            this.eventName = eventName;
            this.handler = handler;
        }
        
        public void Start()
        {
            var tasks = new List<Task>();

            addOrderTask = Task.Run(() => StartListening(AddOrderCommand.Name, MessagePattern.FireAndForget));
            tasks.Add(addOrderTask);
            getOrderTask = Task.Run(() => StartListening(GetOrderCommand.Name, MessagePattern.RequestResponse));
            tasks.Add(getOrderTask);
            removeOrderTask = Task.Run(() => StartListening(RemoveOrderCommand.Name, MessagePattern.RequestResponse));
            tasks.Add(removeOrderTask);

            Task.WaitAll(tasks.ToArray());
        }

        public void Stop()
        {
            // cancel logic for tasks
        }

        private void StartListening(string name, MessagePattern pattern)
        {
            var queue = MessageQueueFactory.CreateInbound(name, pattern);
            Console.WriteLine("Listening on: {0}", queue.Address);
            queue.Listen(x =>
            {
                if (x.BodyType == typeof(AddOrderCommand))
                {
                    AddOrder(x);
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

        private void AddOrder(Message message)
        {
            var order = message.BodyAs<AddOrderCommand>().Order;
            Console.WriteLine("Starting AddOrder for: {0}, at: {1}", order.OrderId, DateTime.Now.TimeOfDay);

            handler.Add(order);

            Console.WriteLine("Order added: {0} at: {1}", order.OrderId, DateTime.Now.TimeOfDay);
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