namespace NProg.Distributed.Service.Messaging
{
    /// <summary>
    /// Class MessageReceiver.
    /// </summary>
    public class MessageReceiver : IMessageReceiver
    {
        /// <summary>
        /// The handler register
        /// </summary>
        private readonly IHandlerRegister handlerRegister;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageReceiver"/> class.
        /// </summary>
        /// <param name="handlerRegister">The handler register.</param>
        public MessageReceiver(IHandlerRegister handlerRegister)
        {
            this.handlerRegister = handlerRegister;
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Message.</returns>
        public Message Send(Message message)
        {
            var messageHandler = handlerRegister.GetHandler(message);
            return messageHandler.Handle(message);
        }
    }
}