using System;
using NetMQ;
using NProg.Distributed.Messaging;
using NProg.Distributed.Messaging.Extensions;

namespace NProg.Distributed.NetMQ.Messaging
{
    public class NmqRequestQueue : IRequestQueue
    {
        private readonly NetMQSocket socket;

        public NmqRequestQueue(NetMQContext context, Uri serviceUri)
        {
            socket = context.CreateRequestSocket();
            var address = string.Format("tcp://{0}:{1}", serviceUri.Host, serviceUri.Port);
            socket.Connect(address);
        }

        public void Send(Message message)
        {
            var json = message.ToJsonString();
            socket.Send(json);
        }

        public void Receive(Action<Message> onMessageReceived)
        {
            string inbound;

            try
            {
                inbound = socket.ReceiveString();
            }
            catch (NetMQException)
            {
                Dispose(true);
                return;
            }

            var message = Message.FromJson(inbound);
            onMessageReceived(message);
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