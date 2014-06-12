using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Messaging.Spec;
using NProg.Distributed.Service;
using NProg.Distributed.ZeroMQ.Command;
using NProg.Distributed.ZeroMQ.Messaging;
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
            var messageQueue = MessageQueueFactory.CreateOutbound(AddOrderCommand.Name, MessagePattern.RequestResponse);
            messageQueue.Send(new Message
            {
                Body = new AddOrderCommand {Order = item}
            });

            var responseQueue = messageQueue.GetResponseQueue();

            responseQueue.Receive(x => Console.WriteLine("Order added: {0}", x.BodyAs<string>()));
        }

        public Order Get(Guid guid)
        {
            var messageQueue = MessageQueueFactory.CreateOutbound(GetOrderCommand.Name, MessagePattern.RequestResponse);
            messageQueue.Send(new Message
            {
                Body = new GetOrderCommand{ OrderId = guid }
            });

            var responseQueue = messageQueue.GetResponseQueue();

            Order order = null;
            responseQueue.Receive(x =>
            {
                order = x.BodyAs<Order>();
            });

            return order;
        }

        public bool Remove(Guid guid)
        {
            var messageQueue = MessageQueueFactory.CreateOutbound(RemoveOrderCommand.Name, MessagePattern.RequestResponse);
            messageQueue.Send(new Message
            {
                Body = new RemoveOrderCommand { OrderId = guid }
            });

            var responseQueue = messageQueue.GetResponseQueue();
            
            var status = string.Empty;
            responseQueue.Receive(x =>
            {
                status = x.BodyAs<string>();
            });

            return Convert.ToBoolean(status);
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