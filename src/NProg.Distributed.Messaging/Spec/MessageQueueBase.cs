using System;
using System.Collections.Generic;
using System.Linq;

namespace NProg.Distributed.Messaging.Spec
{
    public abstract class MessageQueueBase : IMessageQueue
    {
        public string Address { get; protected set; }

        public MessagePattern Pattern { get; protected set; }

        public Dictionary<string, object> Properties { get; protected set; }

        protected Direction Direction { get; set; }

        public abstract void InitialiseOutbound(string name, MessagePattern pattern,
            Dictionary<string, object> properties = null);

        public abstract void InitialiseInbound(string name, MessagePattern pattern,
            Dictionary<string, object> properties = null);

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

        protected void Initialise(Direction direction, string name, MessagePattern pattern,
            Dictionary<string, object> properties = null)
        {
            Direction = direction;
            Pattern = pattern;
            Address = GetAddress(name);
            Properties = properties ?? new Dictionary<string, object>();
        }

        protected void RequireProperty<T>(string name)
        {
            var value = GetPropertyValue<T>(name);
            if (value.Equals(default(T)))
            {
                throw new InvalidOperationException(string.Format("Property named: {0} of type: {1} is required for: {2}", name, typeof(T).Name, Pattern));
            }
        }

        protected T GetPropertyValue<T>(string name)
        {
            T value = default(T);
            if (Properties != null && Properties.Count(x => x.Key == name) == 1 && Properties[name].GetType() == typeof(T))
            {
                value = (T)Properties[name];
            }
            return value;
        }
    }
}