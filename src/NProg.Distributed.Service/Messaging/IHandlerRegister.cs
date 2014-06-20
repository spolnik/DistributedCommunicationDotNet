namespace NProg.Distributed.Service.Messaging
{
    /// <summary>
    /// Interface IHandlerRegister
    /// </summary>
    public interface IHandlerRegister
    {
        /// <summary>
        /// Gets the handler.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>IMessageHandler.</returns>
        IMessageHandler GetHandler(Message message);
    }

}