using System;
using System.Collections.Generic;
using System.Threading;
using NetMQ;
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
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly NetMQContext context;

        public NmqOrderServer(IHandler<Order> handler)
        {
            this.handler = handler;
            cancellationTokenSource = new CancellationTokenSource();
            context = NetMQContext.Create();
        }
        
        public void Start()
        {
            StartListening(AddOrderRequest.Name, MessagePattern.RequestResponse);
            StartListening(GetOrderRequest.Name, MessagePattern.RequestResponse);
            StartListening(RemoveOrderRequest.Name, MessagePattern.RequestResponse);
        }

        public void Stop()
        {
            cancellationTokenSource.Cancel();
            context.Dispose();
        }

        private void StartListening(string name, MessagePattern pattern)
        {
            var queue = MessageQueueFactory.CreateInbound(name, pattern, GetProperties());
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
            }, cancellationTokenSource.Token);
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

        private Dictionary<string, object> GetProperties()
        {
            return new Dictionary<string, Object> {{"context", context}};
        }
    }
}