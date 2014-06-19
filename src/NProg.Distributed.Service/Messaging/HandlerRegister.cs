using System.Collections.Generic;
using System.Linq;

namespace NProg.Distributed.Service.Messaging
{
    public class HandlerRegister : IHandlerRegister
    {
        private readonly List<IMessageHandler> register;

        public HandlerRegister()
        {
            register = new List<IMessageHandler>();
        }

        public void Register(IMessageHandler messageHandler)
        {
            register.Add(messageHandler);
        }

        public IMessageHandler GetHandler(Message message)
        {
            return register.First(x => x.CanHandle(message));
        }
    }
}