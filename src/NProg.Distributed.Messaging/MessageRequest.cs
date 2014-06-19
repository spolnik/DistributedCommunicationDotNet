using System;

namespace NProg.Distributed.Messaging
{
    public abstract class MessageRequest : IMessageRequest
    {
        protected abstract Message SendInternal(Message message);

        public Message Send(Message message)
        {
            try
            {
                return SendInternal(message);
            }
            catch (Exception)
            {
                Dispose(true);
                return null;
            }
        }

        protected abstract void Dispose(bool disposing);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}