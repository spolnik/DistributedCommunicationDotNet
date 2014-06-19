using System;
using NetMQ;
using NProg.Distributed.Messaging;
using NProg.Distributed.Messaging.Extensions;

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

        protected override Message SendInternal(Message message)
        {
            var json = message.ToJsonString();
            socket.Send(json);

            var response = socket.ReceiveString();
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