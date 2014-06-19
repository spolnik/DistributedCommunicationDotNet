using System;
using System.Text;
using NProg.Distributed.Messaging;
using NProg.Distributed.Messaging.Extensions;
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

        protected override Message SendInternal(Message message)
        {
            var json = message.ToJsonString();
            socket.Send(json, Encoding.UTF8);

            var response = socket.Receive(Encoding.UTF8);
            return Message.FromJson(response);
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