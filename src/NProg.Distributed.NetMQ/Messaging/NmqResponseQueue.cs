using System;
using System.Threading;
using NetMQ;
using NProg.Distributed.Messaging;
using NProg.Distributed.Messaging.Extensions;

namespace NProg.Distributed.NetMQ.Messaging
{
    public class NmqResponseQueue : IResponseQueue
    {
        private readonly NetMQSocket socket;

        public NmqResponseQueue(NetMQContext context, int port)
        {
            socket = context.CreateResponseSocket();
            var address = string.Format("tcp://127.0.0.1:{0}", port);
            socket.Bind(address);
        }

        public void Response(Message message)
        {
            var json = message.ToJsonString();
            socket.Send(json);
        }

        public void Listen(Action<Message> onMessageReceived, CancellationTokenSource token)
        {
            socket.ReceiveReady += (sender, args) =>
            {
                var inbound = socket.ReceiveString();

                var message = Message.FromJson(inbound);
                onMessageReceived(message);
            };

            while (!token.IsCancellationRequested)
            {
                socket.Poll();
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