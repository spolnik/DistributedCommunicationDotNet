using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;
using NProg.Distributed.Messaging.Extensions;
using NProg.Distributed.Messaging.Spec;

namespace NProg.Distributed.NetMQ.Messaging
{
    public class NmqMessageQueue : MessageQueueBase
    {
        private NetMQSocket socket;

        public override void InitialiseOutbound(string name, MessagePattern pattern, Dictionary<string, object> properties = null)
        {
            Initialise(Direction.Outbound, name, pattern, properties);
            EnsureContext();
            switch (Pattern)
            {
                case MessagePattern.RequestResponse:
                    socket = GetContext().CreateRequestSocket();
                    socket.Connect(Address);
                    break;

                case MessagePattern.FireAndForget:
                    socket = GetContext().CreatePushSocket();
                    socket.Connect(Address);
                    break;

                case MessagePattern.PublishSubscribe:
                    socket = GetContext().CreatePublisherSocket();
                    socket.Bind(Address);
                    break;
            }
        }

        private NetMQContext GetContext()
        {
            return GetPropertyValue<NetMQContext>("context");
        }

        private void EnsureContext()
        {
            RequireProperty<NetMQContext>("context");
        }

        public override void InitialiseInbound(string name, MessagePattern pattern, Dictionary<string, object> properties = null)
        {
            Initialise(Direction.Inbound, name, pattern, properties);
            EnsureContext();
            switch (Pattern)
            {
                case MessagePattern.RequestResponse:
                    socket = GetContext().CreateResponseSocket();
                    socket.Bind(Address);
                    break;

                case MessagePattern.FireAndForget:
                    socket = GetContext().CreatePullSocket();
                    socket.Bind(Address);
                    break;

                case MessagePattern.PublishSubscribe:
                    socket = GetContext().CreateSubscriberSocket();
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

        public override void Receive(Action<Message> onMessageReceived, bool processAsync, int maximumWaitMilliseconds = 0)
        {
            var inbound = string.Empty;

            try
            {
                inbound = maximumWaitMilliseconds > 0
                    ? socket.ReceiveString(TimeSpan.FromMilliseconds(maximumWaitMilliseconds))
                    : socket.ReceiveString();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error: {0}", exception.Message);
                Console.WriteLine("Closing socket");
                Dispose(true);
            }

            var message = Message.FromJson(inbound);
            //we can only process ZMQ async if the pattern supports it - we can't call Rec
            //twice on a REP socket without the Send in between:
            if (processAsync && Pattern != MessagePattern.RequestResponse)
                Task.Factory.StartNew(() => onMessageReceived(message));
            else
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