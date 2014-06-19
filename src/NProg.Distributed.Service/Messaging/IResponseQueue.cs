using System;
using System.Threading;

namespace NProg.Distributed.Service.Messaging
{
    public interface IResponseQueue : IDisposable
    {
        void Response(Message message);

        void Listen(Action<Message> onMessageReceived, CancellationTokenSource token);
    }
}
