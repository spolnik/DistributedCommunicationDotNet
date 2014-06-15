using System;
using System.Threading.Tasks;
using NProg.Distributed.Domain;
using NProg.Distributed.Messaging.Queries;
using NProg.Distributed.Messaging.Spec;
using NProg.Distributed.Service;
using NProg.Distributed.ZeroMQ.Messaging;

namespace NProg.Distributed.ZeroMQ
{
    public class ZmqOrderServer : IServer
    {
        private readonly IHandler<Order> handler;
        
        public ZmqOrderServer(IHandler<Order> handler)
        {
            this.handler = handler;
        }
        
        public void Start()
        {
            Task.Run(() => StartListening(AddOrderRequest.Name, MessagePattern.RequestResponse));
            Task.Run(() => StartListening(GetOrderRequest.Name, MessagePattern.RequestResponse));
            Task.Run(() => StartListening(RemoveOrderRequest.Name, MessagePattern.RequestResponse));
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
                if (x.BodyType == typeof(AddOrderRequest))
                {
                    AddOrder(x, queue);
                }
                else if (x.BodyType == typeof(GetOrderRequest))
                {
                    GetOrder(x.BodyAs<GetOrderRequest>().OrderId, x, queue);
                }
                else if (x.BodyType == typeof(RemoveOrderRequest))
                {
                    RemoveOrder(x.BodyAs<RemoveOrderRequest>().OrderId, x, queue);
                }
            });
        }

        private void AddOrder(Message message, IMessageQueue queue)
        {
            var order = message.BodyAs<AddOrderRequest>().Order;
            Console.WriteLine("Starting AddOrder for: {0}, at: {1}", order.OrderId, DateTime.Now.TimeOfDay);

            handler.Add(order);

            var responseQueue = queue.GetReplyQueue(message);

            responseQueue.Send(new Message
            {
                Body = new StatusResponse {Status = true}
            });

            Console.WriteLine("Order added: {0} at: {1}", order.OrderId, DateTime.Now.TimeOfDay);
        }

        private void GetOrder(Guid orderId, Message message, IMessageQueue queue)
        {
            Console.WriteLine("Starting GetOrder for: {0}, at: {1}", orderId, DateTime.Now.TimeOfDay);

            var order = handler.Get(orderId);
            var responseQueue = queue.GetReplyQueue(message);

            responseQueue.Send(new Message
            {
                Body = new GetOrderResponse {Order = order}
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
                Body = new StatusResponse { Status = status }
            });

            Console.WriteLine("Returned: {0} for RemoveOrder, to: {1}, at: {2}", status, responseQueue.Address,
                DateTime.Now.TimeOfDay);
        }
    }
}