using System;
using System.Text;
using NProg.Distributed.Messaging;
using ZeroMQ;

namespace NProg.Distributed.ZeroMQ.Messaging
{
    public class ZmqMessageRequest : MessageRequest
    {
        private readonly ZmqSocket socket;

        public ZmqMessageRequest(ZmqContext context, Uri serviceUri)
        {
            socket = context.CreateSocket(SocketType.REQ);
            var address = string.Format("tcp://{0}:{1}", serviceUri.Host, serviceUri.Port);
            socket.Connect(address);
        }

        protected override void Request(string message)
        {
            socket.Send(message, Encoding.UTF8);
        }

        protected override string Response()
        {
            return socket.Receive(Encoding.UTF8);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && socket != null)
            {
                socket.Dispose();
            }
        }
    }
}