using System;
using NProg.Distributed.Messaging.Extensions;

namespace NProg.Distributed.Messaging
{
    public abstract class MessageRequest : IMessageRequest
    {
        protected abstract void Request(string message);

        protected abstract string Response();

        public Message Send(Message message)
        {
            var json = message.ToJsonString();
            Request(json);

            string inbound;

            try
            {
                inbound = Response();
            }
            catch (Exception)
            {
                Dispose(true);
                return null;
            }

            return Message.FromJson(inbound);
        }

        protected abstract void Dispose(bool disposing);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}