using System;
using NetMQ;
using NetMQ.Sockets;
using NProg.Distributed.Messaging.Extensions;
using NProg.Distributed.Messaging.Spec;

namespace NProg.Distributed.NetMQ.Messaging
{
    public class NmqMessageQueue : MessageQueueBase
    {
        private static volatile NetMQContext context;
        private static readonly object ContextLock = new object();
        private NetMQSocket socket;

        public override void InitialiseOutbound(string name, MessagePattern pattern)
        {
            Initialise(Direction.Outbound, name, pattern);
            EnsureContext();
            switch (Pattern)
            {
                case MessagePattern.RequestResponse:
                    socket = context.CreateRequestSocket();
                    socket.Connect(Address);
                    break;

                case MessagePattern.FireAndForget:
                    socket = context.CreatePushSocket();
                    socket.Connect(Address);
                    break;

                case MessagePattern.PublishSubscribe:
                    socket = context.CreatePublisherSocket();
                    socket.Bind(Address);
                    break;
            }
        }

        public override void InitialiseInbound(string name, MessagePattern pattern)
        {
            Initialise(Direction.Inbound, name, pattern);
            EnsureContext();
            switch (Pattern)
            {
                case MessagePattern.RequestResponse:
                    socket = context.CreateResponseSocket();
                    socket.Bind(Address);
                    break;

                case MessagePattern.FireAndForget:
                    socket = context.CreatePullSocket();
                    socket.Bind(Address);
                    break;

                case MessagePattern.PublishSubscribe:
                    socket = context.CreateSubscriberSocket();
                    socket.Connect(Address);
                    ((SubscriberSocket)socket).Subscribe("");
                    break;
            }
        }

        public override void Send(Message message)
        {
            var json = message.ToJsonString();
            socket.Send(json);
        }

        public override void Listen(Action<Message> onMessageReceived)
        {
            while (true)
            {
                Receive(onMessageReceived);
            }
        }

        public override void Receive(Action<Message> onMessageReceived)
        {
            var inbound = socket.ReceiveString();
            var message = Message.FromJson(inbound);
            onMessageReceived(message);
        }

        protected override string GetAddress(string name)
        {
            switch (name.ToLower())
            {
                case "add-order":
                    return "tcp://127.0.0.1:55001";

                case "get-order":
                    return "tcp://127.0.0.1:55002";

                case "remove-order":
                    return "tcp://127.0.0.1:55003";

                default:
                    throw new ArgumentException(string.Format("Unknown queue name: {0}", name), "name");
            }
        }

        public override IMessageQueue GetResponseQueue()
        {
            return this;
        }

        public override IMessageQueue GetReplyQueue(Message message)
        {
            return this;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && socket != null)
            {
                socket.Dispose();
            }
        }

        private static void EnsureContext()
        {
            if (context == null)
            {
                lock (ContextLock)
                {
                    if (context == null)
                    {
                        context = NetMQContext.Create();
                    }
                }
            }
        }
    }
}