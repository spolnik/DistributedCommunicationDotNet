using System;
using System.Messaging;
using NProg.Distributed.Messaging.Extensions;
using NProg.Distributed.Messaging.Spec;
using Message = NProg.Distributed.Messaging.Spec.Message;

namespace NProg.Distributed.Msmq.Messaging
{
    internal class MsmqMessageQueue : MessageQueueBase
    {
        private MessageQueue queue;
        private bool useTemporaryQueue;

        public override void InitialiseOutbound(string name, MessagePattern pattern)
        {
            Initialise(Direction.Outbound, name, pattern);
            queue = new MessageQueue(Address);
        }

        public override void InitialiseInbound(string name, MessagePattern pattern)
        {
            Initialise(Direction.Inbound, name, pattern);
            switch (Pattern)
            {
                case MessagePattern.PublishSubscribe:
                    queue = new MessageQueue(Address);
                    break;

                case MessagePattern.RequestResponse:
                    queue = useTemporaryQueue ? MessageQueue.Create(Address) : new MessageQueue(Address);
                    break;

                default:
                    queue = new MessageQueue(Address);
                    break;
            }
        }

        public override void Send(Message message)
        {
            var outbound = new System.Messaging.Message {BodyStream = message.ToJsonStream()};

            if (!string.IsNullOrEmpty(message.ResponseAddress))
                outbound.ResponseQueue = new MessageQueue(message.ResponseAddress);

            queue.Send(outbound);
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
            var inbound = queue.Receive();
            var message = Message.FromJson(inbound.BodyStream);
            onMessageReceived(message);
        }

        public override string GetAddress(string name)
        {
            if (Pattern == MessagePattern.RequestResponse && Direction == Direction.Inbound && string.IsNullOrEmpty(name))
            {
                useTemporaryQueue = true;
                return string.Format(".\\private$\\{0}", Guid.NewGuid().ToString().Substring(0, 6));
            }

            switch (name.ToLower())
            {
                case "add-order":
                    return ".\\private$\\nprog.messagequeue.add-order";

                case "get-order":
                    return ".\\private$\\nprog.messagequeue.get-order";

                case "remove-order":
                    return ".\\private$\\nprog.messagequeue.remove-order";

                default:
                    return name;
            }
        }

        public override IMessageQueue GetResponseQueue()
        {
            if (!(Pattern == MessagePattern.RequestResponse && Direction == Direction.Outbound))
                throw new InvalidOperationException("Cannot get a response queue except for outbound request-response");

            var responseQueue = new MsmqMessageQueue();
            responseQueue.InitialiseInbound(null, MessagePattern.RequestResponse);
            return responseQueue;
        }

        public override IMessageQueue GetReplyQueue(Message message)
        {
            if (!(Pattern == MessagePattern.RequestResponse && Direction == Direction.Inbound))
                throw new InvalidOperationException("Cannot get a reply queue except for inbound request-response");

            var responseQueue = new MsmqMessageQueue();
            responseQueue.InitialiseOutbound(message.ResponseAddress, MessagePattern.RequestResponse);
            return responseQueue;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && queue != null)
            {
                queue.Dispose();
                if (useTemporaryQueue && MessageQueue.Exists(Address))
                {
                    MessageQueue.Delete(Address);
                }
            }
        }
    }
}