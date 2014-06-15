using System;
using NetMQ;
using NProg.Distributed.Messaging;
using NProg.Distributed.Messaging.Extensions;

namespace NProg.Distributed.NetMQ.Messaging
{
    public class NmqResponseQueue : IMessageQueue
    {
        private readonly NetMQSocket socket;

        public NmqResponseQueue(NetMQContext context, int port)
        {
            socket = context.CreateResponseSocket();
            var address = string.Format("tcp://127.0.0.1:{0}", port);
            socket.Bind(address);
        }

        public void Send(Message message)
        {
            var json = message.ToJsonString();
            socket.Send(json);
        }

        public void Listen(Action<Message> onMessageReceived)
        {
            while (true)
            {
                Receive(onMessageReceived);
            }
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