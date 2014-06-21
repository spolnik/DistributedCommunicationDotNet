namespace NProg.Distributed.Common.Service.Messaging
{
    /// <summary>
    /// Interface IMessageHandler
    /// </summary>
    public interface IMessageHandler
    {
        /// <summary>
        /// Determines whether this instance can handle the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns><c>true</c> if this instance can handle the specified message; otherwise, <c>false</c>.</returns>
        bool CanHandle(Message message);

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Message.</returns>
        Message Handle(Message message);
    }
}