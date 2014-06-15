using System;

namespace NProg.Distributed.Messaging.Spec
{
    public abstract class MessageQueueBase : IMessageQueue
    {

        protected MessagePattern Pattern { get; private set; }

        protected Direction Direction { get; set; }
        public string Address { get; private set; }

        public abstract void InitialiseOutbound(string name, MessagePattern pattern);

        public abstract void InitialiseInbound(string name, MessagePattern pattern);

        public abstract void Send(Message message);

        public abstract void Listen(Action<Message> onMessageReceived);

        public abstract void Receive(Action<Message> onMessageReceived);

        public abstract IMessageQueue GetResponseQueue();

        public abstract IMessageQueue GetReplyQueue(Message message);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract string GetAddress(string name);

        protected abstract void Dispose(bool disposing);

        protected void Initialise(Direction direction, string name, MessagePattern pattern)
        {
            Direction = direction;
            Pattern = pattern;
            Address = GetAddress(name);
        }
    }
}