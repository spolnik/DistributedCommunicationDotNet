using System;

namespace NProg.Distributed.Core.Service.Messaging
{
    /// <summary>
    /// Class MessageSenderBase.
    /// </summary>
    public abstract class MessageSenderBase : IMessageSender
    {
        /// <summary>
        /// Sends the internal.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Message.</returns>
        protected abstract Message SendInternal(Message message);

        /// <summary>
        /// Sends the specified request.
        /// </summary>
        /// <typeparam name="TRequest">The type of the t request.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns>Message.</returns>
        /// <exception cref="System.ArgumentException">Request cannot be Message instance;request</exception>
        public Message Send<TRequest>(TRequest request) where TRequest : IMessage 
        {
            var message = Message.From(request);

            try
            {
                return SendInternal(message);
            }
            catch (MessageException)
            {
                throw;
            }
            catch (Exception)
            {
                Dispose(true);
                return null;
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected abstract void Dispose(bool disposing);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}