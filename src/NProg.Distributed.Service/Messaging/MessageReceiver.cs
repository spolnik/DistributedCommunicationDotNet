using System.Collections.Generic;
using System.Linq;

namespace NProg.Distributed.Common.Service.Messaging
{
    /// <summary>
    /// Class MessageReceiver.
    /// </summary>
    public sealed class MessageReceiver : IMessageReceiver
    {
        /// <summary>
        /// The register
        /// </summary>
        private readonly IEnumerable<IMessageHandler> messageHandlers;

        public MessageReceiver(IEnumerable<IMessageHandler> messageHandlers)
        {
            this.messageHandlers = messageHandlers;
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Message.</returns>
        public Message Send(Message message)
        {
            var messageHandler = GetHandler(message);
            return messageHandler.Handle(message);
        }

        private IMessageHandler GetHandler(Message message)
        {
            return messageHandlers.First(x => x.CanHandle(message));
        }
    }
}