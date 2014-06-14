using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NProg.Distributed.Messaging.Spec
{
    public abstract class MessageQueueBase : IMessageQueue
    {
        protected bool isListening;
        protected int pollingInterval = 100;

        protected MessagePattern Pattern { get; private set; }

        public Dictionary<string, object> Properties { get; protected set; }
        protected Direction Direction { get; set; }
        public string Address { get; private set; }

        public abstract void InitialiseOutbound(string name, MessagePattern pattern,
            Dictionary<string, object> properties = null);

        public abstract void InitialiseInbound(string name, MessagePattern pattern,
            Dictionary<string, object> properties = null);

        public abstract void Send(Message message);

        public virtual void Listen(Action<Message> onMessageReceived, CancellationToken cancellationToken)
        {
            if (isListening)
                return;

            Task.Factory.StartNew(() => ListenInternal(onMessageReceived, cancellationToken), cancellationToken,
                TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        protected virtual void ListenInternal(Action<Message> onMessageReceived, CancellationToken cancellationToken)
        {
            isListening = true;
            while (isListening)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    isListening = false;
                    cancellationToken.ThrowIfCancellationRequested();
                }
                try
                {
                    Receive(onMessageReceived, true);
                    Thread.Sleep(pollingInterval);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: {0}", ex);
                }
            }
        }
        
        public virtual void Receive(Action<Message> onMessageReceived, int maximumWaitMilliseconds = 0)
        {
            Receive(onMessageReceived, false, maximumWaitMilliseconds);
        }

        public abstract void Receive(Action<Message> onMessageReceived, bool processAsync, int maximumWaitMilliseconds = 0);

        public abstract IMessageQueue GetResponseQueue();

        public abstract IMessageQueue GetReplyQueue(Message message);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract string GetAddress(string name);

        protected abstract void Dispose(bool disposing);

        protected void Initialise(Direction direction, string name, MessagePattern pattern, Dictionary<string, object> properties)
        {
            Direction = direction;
            Pattern = pattern;
            Address = GetAddress(name);
            Properties = properties ?? new Dictionary<string, object>();
        }

        protected void RequireProperty<T>(string name)
        {
            var value = GetPropertyValue<T>(name);
            if (value == null || value.Equals(default(T)))
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