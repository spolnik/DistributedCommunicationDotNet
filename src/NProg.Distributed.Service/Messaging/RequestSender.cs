using System;

namespace NProg.Distributed.Service.Messaging
{
    public abstract class RequestSender : IRequestSender
    {
        protected abstract Message SendInternal(Message message);

        public Message Send<TRequest>(TRequest request) where TRequest : class 
        {
            if (request is Message)
                throw new ArgumentException("Request cannot be Message instance", "request");

            var message = Message.From(request);

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