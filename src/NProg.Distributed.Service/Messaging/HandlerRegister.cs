using System.Collections.Generic;
using System.Linq;

namespace NProg.Distributed.Service.Messaging
{
    public class HandlerRegister : IHandlerRegister
    {
        private readonly IList<IMessageHandler> register;

        public HandlerRegister(IList<IMessageHandler> register)
        {
            this.register = register;
        }

        public IMessageHandler GetHandler(Message message)
        {
            return register.First(x => x.CanHandle(message));
        }
    }
}