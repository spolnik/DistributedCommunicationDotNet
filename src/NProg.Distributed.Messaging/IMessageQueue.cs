using System;
using System.Threading;

namespace NProg.Distributed.Messaging
{
    public interface IRequestQueue : IDisposable
    {
        void Send(Message message);
        
        void Receive(Action<Message> onMessageReceived);
    }

    public interface IResponseQueue : IDisposable
    {
        void Send(Message message);

        void Listen(Action<Message> onMessageReceived, CancellationTokenSource token);

        void Receive(Action<Message> onMessageReceived);
    }

}
