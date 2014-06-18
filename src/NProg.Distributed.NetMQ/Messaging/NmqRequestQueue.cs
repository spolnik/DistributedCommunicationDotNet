using System;
using NetMQ;
using NProg.Distributed.Messaging;

namespace NProg.Distributed.NetMQ.Messaging
{
    public class NmqMessageRequest : MessageRequest
    {
        private readonly NetMQSocket socket;

        public NmqMessageRequest(NetMQContext context, Uri serviceUri)
        {
            socket = context.CreateRequestSocket();
            var address = string.Format("tcp://{0}:{1}", serviceUri.Host, serviceUri.Port);
            socket.Connect(address);
        }

        protected override void Request(string message)
        {
            socket.Send(message);
        }

        protected override string Response()
        {
            return socket.ReceiveString();
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