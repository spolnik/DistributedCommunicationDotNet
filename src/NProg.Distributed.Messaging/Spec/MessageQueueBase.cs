using System;
using System.Threading;
using System.Threading.Tasks;

namespace NProg.Distributed.Messaging.Spec
{
    public abstract class MessageQueueBase : IMessageQueue
    {
        protected bool isListening;
        protected int pollingInterval = 100;

        protected MessagePattern Pattern { get; private set; }

        protected Direction Direction { get; set; }
        public string Address { get; private set; }

        public abstract void InitialiseOutbound(string name, MessagePattern pattern);

        public abstract void InitialiseInbound(string name, MessagePattern pattern);

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

        protected void Initialise(Direction direction, string name, MessagePattern pattern)
        {
            Direction = direction;
            Pattern = pattern;
            Address = GetAddress(name);
        }
    }
}