using System;
using NProg.Distributed.Core.Data;

namespace NProg.Distributed.Core.Service.Messaging
{
    public abstract class MessageHandlerBase<TRequest, TRepository> : IMessageHandler 
        where TRequest : IRequestResponse
        where TRepository : IDataRepository
    {
        protected TRepository repository;

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        protected MessageHandlerBase(TRepository repository)
        {
            this.repository = repository;
        }

        #region Implementation of IMessageHandler

        /// <summary>
        /// Determines whether this instance can handle the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns><c>true</c> if this instance can handle the specified message; otherwise, <c>false</c>.</returns>
        public virtual bool CanHandle(Message message)
        {
            return message.BodyType == typeof (TRequest);
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Message.</returns>
        public Message Handle(Message message)
        {
            var request = message.Receive<TRequest>();
            try
            {
                var requestResponse = Process(request);
                return Message.From(requestResponse);
            }
            catch (Exception exception)
            {
                return Message.ErrorFrom(exception);
            }
        }

        #endregion

        protected abstract IRequestResponse Process(TRequest request);
    }
}