using System;
using System.Text;
using NProg.Distributed.Messaging.Extensions;
using NProg.Distributed.Messaging.Spec;
using ZMQ;
using Exception = ZMQ.Exception;

namespace NProg.Distributed.ZeroMQ.Messaging
{
    public class ZmqMessageQueue : MessageQueueBase
    {
        private static volatile Context context;
        private static readonly object ContextLock = new object();
        private Socket socket;

        public override void InitialiseOutbound(string name, MessagePattern pattern)
        {
            Initialise(Direction.Outbound, name, pattern);
            EnsureContext();
            switch (Pattern)
            {
                case MessagePattern.RequestResponse:
                    socket = context.Socket(SocketType.REQ);
                    socket.Connect(Address);
                    break;

                case MessagePattern.FireAndForget:
                    socket = context.Socket(SocketType.PUSH);
                    socket.Connect(Address);
                    break;

                case MessagePattern.PublishSubscribe:
                    socket = context.Socket(SocketType.PUB);
                    socket.Bind(Address);
                    break;
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
                        context = new Context();
                    }
                }
            }
        }

        public override void InitialiseInbound(string name, MessagePattern pattern)
        {
            Initialise(Direction.Inbound, name, pattern);
            EnsureContext();
            switch (Pattern)
            {
                case MessagePattern.RequestResponse:
                    socket = context.Socket(SocketType.REP);
                    socket.Bind(Address);
                    break;

                case MessagePattern.FireAndForget:
                    socket = context.Socket(SocketType.PULL);
                    socket.Bind(Address);
                    break;

                case MessagePattern.PublishSubscribe:
                    socket = context.Socket(SocketType.SUB);
                    socket.Connect(Address);
                    socket.Subscribe("", Encoding.UTF8);
                    break;
            }
        }

        public override void Send(Message message)
        {
            var json = message.ToJsonString();
            socket.Send(json, Encoding.UTF8);
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
            string inbound;

            try
            {
                inbound = socket.Recv(Encoding.UTF8);
            }
            catch (Exception)
            {
                Dispose(true);
                return;
            }

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
    }
}