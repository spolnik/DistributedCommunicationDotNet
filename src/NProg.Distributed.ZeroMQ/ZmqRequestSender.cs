﻿using System;
using System.Text;
using NProg.Distributed.Common.Service.Extensions;
using NProg.Distributed.Common.Service.Messaging;
using ZeroMQ;

namespace NProg.Distributed.Transport.ZeroMQ
{
    internal sealed class ZmqRequestSender : RequestSenderBase
    {
        private ZmqSocket socket;
        private ZmqContext context;

        internal ZmqRequestSender(Uri serviceUri)
        {
            context = ZmqContext.Create();
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