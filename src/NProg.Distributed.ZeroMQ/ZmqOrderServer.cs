using System;
using System.Threading;
using System.Threading.Tasks;
using NProg.Distributed.Domain;
using NProg.Distributed.Messaging;
using NProg.Distributed.Messaging.Queries;
using NProg.Distributed.Service;
using NProg.Distributed.ZeroMQ.Messaging;
using ZeroMQ;

namespace NProg.Distributed.ZeroMQ
{
    public class ZmqOrderServer : IServer, IDisposable
    {
        private readonly int port;

        private readonly IHandler<Guid, Order> handler;
        private readonly ZmqContext context;
        private readonly ZmqResponseQueue responseQueue;
        private readonly CancellationTokenSource token;

        public ZmqOrderServer(IHandler<Guid, Order> handler, int port)
        {
            token = new CancellationTokenSource();

            this.port = port;
            this.handler = handler;
            context = ZmqContext.Create();
            context.ThreadPoolSize = 4;
            responseQueue = new ZmqResponseQueue(context, port);
        }
        
        public void Start()
        {
            Task.Run(() => StartListening());
        }

        public void Stop()
        {
            Dispose(true);
        }

        private void StartListening()
        {
            Console.WriteLine("Listening on: tcp://127.0.0.1:{0}", port);
            responseQueue.Listen(x =>
            {
                if (x.BodyType == typeof(AddOrderRequest))
                {
                    AddOrder(x);
                }
                else if (x.BodyType == typeof(GetOrderRequest))
                {
                    GetOrder(x.Receive<GetOrderRequest>().OrderId);
                }
                else if (x.BodyType == typeof(RemoveOrderRequest))
                {
                    RemoveOrder(x.Receive<RemoveOrderRequest>().OrderId);
                }
            }, token);
        }

        private void AddOrder(Message message)
        {
            var order = message.Receive<AddOrderRequest>().Order;
            handler.Add(order.OrderId, order);

            responseQueue.Response(new Message
            {
                Body = new StatusResponse {Status = true}
            });
        }

        private void GetOrder(Guid orderId)
        {
            var order = handler.Get(orderId);

            responseQueue.Response(new Message
            {
                Body = new GetOrderResponse {Order = order}
            });
        }

        private void RemoveOrder(Guid orderId)
        {
            var status = handler.Remove(orderId);
            
            responseQueue.Response(new Message
            {
                Body = new StatusResponse { Status = status }
            });
        }

        protected void Dispose(bool disposing)
        {
            if (disposing && responseQueue != null)
                responseQueue.Dispose();

            if (disposing && context != null)
                context.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}