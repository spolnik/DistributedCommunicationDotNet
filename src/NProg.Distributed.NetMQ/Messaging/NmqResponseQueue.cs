﻿using System;
using System.Threading;
using NetMQ;
using NProg.Distributed.Messaging;
using NProg.Distributed.Messaging.Extensions;

namespace NProg.Distributed.NetMQ.Messaging
{
    public class NmqResponseQueue : IMessageQueue
    {
        private readonly CancellationTokenSource token;
        private readonly NetMQSocket socket;

        public NmqResponseQueue(NetMQContext context, int port, CancellationTokenSource token)
        {
            this.token = token;

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