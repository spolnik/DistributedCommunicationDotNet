using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NProg.Distributed.Service.Extensions;
using NProg.Distributed.Service.Messaging;
using ZeroMQ;

namespace NProg.Distributed.ZeroMQ
{
    public class ZmqResponseQueue : IResponseQueue
    {
        private readonly ZmqSocket socket;

        public ZmqResponseQueue(ZmqContext context, int port)
        {
            socket = context.CreateSocket(SocketType.REP);
            var address = string.Format("tcp://127.0.0.1:{0}", port);
            socket.Bind(address);
        }

        public void Response(Message message)
        {
            var json = message.ToJsonString();
            socket.Send(json, Encoding.UTF8);
        }

        public void Listen(Action<Message> onMessageReceived, CancellationTokenSource token)
        {
            socket.ReceiveReady += (sender, args) =>
            {
                var inbound = socket.Receive(Encoding.UTF8);

                var message = Message.FromJson(inbound);
                onMessageReceived(message);
            };

            var poller = new Poller(new List<ZmqSocket> { socket });

            while (!token.IsCancellationRequested)
            {
                poller.Poll();
            }
        }

        protected void Dispose(bool disposing)
        {
            if (disposing && socket != null)
            {
                socket.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}