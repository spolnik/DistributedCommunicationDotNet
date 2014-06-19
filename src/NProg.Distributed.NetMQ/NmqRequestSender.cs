using System;
using NetMQ;
using NProg.Distributed.Service.Extensions;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.NetMQ
{
    public class NmqRequestSender : RequestSender
    {
        private NetMQSocket socket;
        private NetMQContext context;

        public NmqRequestSender(Uri serviceUri)
        {
            context = NetMQContext.Create();
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
                socket = null;
            }

            if (disposing && context != null)
            {
                context.Dispose();
                context = null;
            }
        }
    }
}