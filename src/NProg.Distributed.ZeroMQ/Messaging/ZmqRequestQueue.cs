using System;
using System.Collections.Generic;
using System.Text;
using NProg.Distributed.Messaging;
using NProg.Distributed.Messaging.Extensions;
using ZeroMQ;

namespace NProg.Distributed.ZeroMQ.Messaging
{
    public class ZmqRequestQueue : IRequestQueue
    {
        private readonly ZmqSocket socket;
        
        public ZmqRequestQueue(ZmqContext context, Uri serviceUri)
        {
            socket = context.CreateSocket(SocketType.REQ);
            var address = string.Format("tcp://{0}:{1}", serviceUri.Host, serviceUri.Port);
            socket.Connect(address);
        }

        public void Send(Message message)
        {
            var json = message.ToJsonString();
            socket.Send(json, Encoding.UTF8);
        }

        public void Receive(Action<Message> onMessageReceived)
        {
            string inbound;

            try
            {
                inbound = socket.Receive(Encoding.UTF8);
            }
            catch (Exception)
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