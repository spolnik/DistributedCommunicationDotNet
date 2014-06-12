using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Messaging.Spec;
using NProg.Distributed.Service;
using NProg.Distributed.ZeroMQ.Messaging;
using NProg.Distributed.ZeroMQ.Queries;
using ZMQ;

namespace NProg.Distributed.ZeroMQ
{
    public class ZmqOrderClient : IHandler<Order>, IDisposable
    {
        private static volatile Context context;
        private static readonly object ContextLock = new object();
        private readonly Socket pushSocket;
        private readonly Socket requestSocket;

        public ZmqOrderClient(Uri serviceUri)
        {
            EnsureContext();
            pushSocket = context.Socket(SocketType.PUSH);
            pushSocket.Connect(GetAddress(serviceUri.Host, serviceUri.Port));

            requestSocket = context.Socket(SocketType.REQ);
            requestSocket.Connect(GetAddress(serviceUri.Host, serviceUri.Port+1));
        }

        public void Add(Order item)
        {
            var messageQueue = MessageQueueFactory.CreateOutbound(AddOrderRequest.Name, MessagePattern.RequestResponse);
            messageQueue.Send(new Message
            {
                Body = new AddOrderRequest {Order = item}
            });

            var responseQueue = messageQueue.GetResponseQueue();

            responseQueue.Receive(x => Console.WriteLine("Order added: {0}", x.BodyAs<StatusResponse>().Status));
        }

        public Order Get(Guid guid)
        {
            var messageQueue = MessageQueueFactory.CreateOutbound(GetOrderRequest.Name, MessagePattern.RequestResponse);
            messageQueue.Send(new Message
            {
                Body = new GetOrderRequest{ OrderId = guid }
            });

            var responseQueue = messageQueue.GetResponseQueue();

            Order order = null;
            responseQueue.Receive(x =>
            {
                order = x.BodyAs<GetOrderResponse>().Order;
            });

            return order;
        }

        public bool Remove(Guid guid)
        {
            var messageQueue = MessageQueueFactory.CreateOutbound(RemoveOrderRequest.Name, MessagePattern.RequestResponse);
            messageQueue.Send(new Message
            {
                Body = new RemoveOrderRequest { OrderId = guid }
            });

            var responseQueue = messageQueue.GetResponseQueue();
            
            var status = false;
            responseQueue.Receive(x =>
            {
                status = x.BodyAs<StatusResponse>().Status;
            });

            return status;
        }

        private static void EnsureContext()
        {
            if (context == null)
            {
                lock (ContextLock)
                {
                    if (context == null)
                    {
                        context = new Context();
                    }
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && requestSocket != null)
                requestSocket.Dispose();

            if (disposing && pushSocket != null)
                pushSocket.Dispose();
        }

        private static string GetAddress(string host, int port)
        {
            return string.Format("tcp://{0}:{1}", host, port);
        }
    }
}