using System;

namespace NProg.Distributed.Messaging.Spec
{
    public abstract class MessageQueueBase : IMessageQueue
    {
        public string Address { get; private set; }

        protected MessagePattern Pattern { get; private set; }

        protected Direction Direction { get; set; }

        public abstract void InitialiseOutbound(string name, MessagePattern pattern);

        public abstract void InitialiseInbound(string name, MessagePattern pattern);

        public abstract void Send(Message message);

        public abstract void Listen(Action<Message> onMessageReceived);

        public abstract void Receive(Action<Message> onMessageReceived);

        public abstract string GetAddress(string name);

        public abstract IMessageQueue GetResponseQueue();

        public abstract IMessageQueue GetReplyQueue(Message message);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposing);

        protected void Initialise(Direction direction, string name, MessagePattern pattern)
        {
            Direction = direction;
            Pattern = pattern;
            Address = GetAddress(name);
        }
    }
}