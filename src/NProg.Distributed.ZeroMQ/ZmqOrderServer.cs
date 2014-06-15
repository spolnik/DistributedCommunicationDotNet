using System;
using System.Threading.Tasks;
using NProg.Distributed.Domain;
using NProg.Distributed.Messaging;
using NProg.Distributed.Messaging.Queries;
using NProg.Distributed.Service;
using NProg.Distributed.ZeroMQ.Messaging;
using ZMQ;

namespace NProg.Distributed.ZeroMQ
{
    public class ZmqOrderServer : IServer, IDisposable
    {
        private readonly int port;

        private readonly IHandler<Order> handler;
        private readonly Context context;
        private readonly ZmqResponseQueue responseQueue;

        public ZmqOrderServer(IHandler<Order> handler, int port)
        {
            this.port = port;
            this.handler = handler;
            context = new Context(4);
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
                    GetOrder(x.BodyAs<GetOrderRequest>().OrderId);
                }
                else if (x.BodyType == typeof(RemoveOrderRequest))
                {
                    RemoveOrder(x.BodyAs<RemoveOrderRequest>().OrderId);
                }
            });
        }

        private void AddOrder(Message message)
        {
            handler.Add(message.BodyAs<AddOrderRequest>().Order);

            responseQueue.Send(new Message
            {
                Body = new StatusResponse {Status = true}
            });
        }

        private void GetOrder(Guid orderId)
        {
            var order = handler.Get(orderId);

            responseQueue.Send(new Message
            {
                Body = new GetOrderResponse {Order = order}
            });
        }

        private void RemoveOrder(Guid orderId)
        {
            var status = handler.Remove(orderId);
            
            responseQueue.Send(new Message
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