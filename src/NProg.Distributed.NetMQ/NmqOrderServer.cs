using System;
using System.Threading.Tasks;
using NProg.Distributed.Domain;
using NProg.Distributed.Messaging.Queries;
using NProg.Distributed.Messaging.Spec;
using NProg.Distributed.NetMQ.Messaging;
using NProg.Distributed.Service;

namespace NProg.Distributed.NetMQ
{
    public class NmqOrderServer : IServer
    {
        private readonly IHandler<Order> handler;

        public NmqOrderServer(IHandler<Order> handler)
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
            handler.Add(order);

            var responseQueue = queue.GetReplyQueue(message);
            responseQueue.Send(new Message
            {
                Body = new StatusResponse {Status = true}
            });
        }

        private void GetOrder(Guid orderId, Message message, IMessageQueue queue)
        {
            var order = handler.Get(orderId);
            var responseQueue = queue.GetReplyQueue(message);

            responseQueue.Send(new Message
            {
                Body = new GetOrderResponse {Order = order}
            });
        }

        private void RemoveOrder(Guid orderId, Message message, IMessageQueue queue)
        {
            var status = handler.Remove(orderId);
            var responseQueue = queue.GetReplyQueue(message);

            responseQueue.Send(new Message
            {
                Body = new StatusResponse { Status = status }
            });
        }
    }
}