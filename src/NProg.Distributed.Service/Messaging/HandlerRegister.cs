using System.Collections.Generic;
using System.Linq;

namespace NProg.Distributed.Service.Messaging
{
    /// <summary>
    /// Class HandlerRegister.
    /// </summary>
    public class HandlerRegister : IHandlerRegister
    {
        /// <summary>
        /// The register
        /// </summary>
        private readonly IList<IMessageHandler> register;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerRegister"/> class.
        /// </summary>
        /// <param name="register">The register.</param>
        public HandlerRegister(IList<IMessageHandler> register)
        {
            this.register = register;
        }

        /// <summary>
        /// Gets the handler.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>IMessageHandler.</returns>
        public IMessageHandler GetHandler(Message message)
        {
            return register.First(x => x.CanHandle(message));
        }
    }
}