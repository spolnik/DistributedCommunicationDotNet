using System;

namespace NProg.Distributed.Messaging
{
    public interface IMessageQueue : IDisposable
    {
        void Send(Message message);
        
        void Listen(Action<Message> onMessageReceived);

        void Receive(Action<Message> onMessageReceived);
    }

}
