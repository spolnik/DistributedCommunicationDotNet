using System;
using System.Text;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;
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
            pushSocket.Connect(serviceUri.ToString());

            requestSocket = context.Socket(SocketType.REQ);
            requestSocket.Connect(serviceUri.ToString());
        }

        public void Add(Order item)
        {
            var json = item.ToJsonString();
            pushSocket.Send(json, Encoding.UTF8);
        }

        public Order Get(Guid guid)
        {
            var json = guid.ToJsonString();
            requestSocket.Send(json, Encoding.UTF8);
            var result = requestSocket.Recv(Encoding.UTF8);
            return result.ReadFromJson<Order>();
        }

        public bool Remove(Guid guid)
        {
            var json = guid.ToJsonString();
            requestSocket.Send(json, Encoding.UTF8);
            var result = requestSocket.Recv(Encoding.UTF8);
            return result.ReadFromJson<bool>();
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
    }

}