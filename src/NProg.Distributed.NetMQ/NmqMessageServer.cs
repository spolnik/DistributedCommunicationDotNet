using System;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.NetMQ
{
    public class NmqMessageServer : IServer, IDisposable
    {
        private readonly int port;

        private readonly IMessageReceiver messageReceiver;
        private NetMQContext context;
        private NmqResponseQueue responseQueue;
        private readonly CancellationTokenSource token;

        public NmqMessageServer(IMessageReceiver messageReceiver, int port)
        {
            token = new CancellationTokenSource();

            this.port = port;
            this.messageReceiver = messageReceiver;
            context = NetMQContext.Create();
            responseQueue = new NmqResponseQueue(context, port);
        }
        
        public void Start()
        {
            Task.Run(() => StartListening());
        }

        public void Stop()
        {
            token.Cancel();
            Dispose(true);
        }

        private void StartListening()
        {
            Console.WriteLine("Listening on: tcp://127.0.0.1:{0}", port);
            responseQueue.Listen(x =>
            {
                var message = messageReceiver.Send(x);
                responseQueue.Response(message);
            }, token);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing && responseQueue != null)
            {
                responseQueue.Dispose();
                responseQueue = null;
            }

            if (disposing && context != null)
            {
                context.Dispose();
                context = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}