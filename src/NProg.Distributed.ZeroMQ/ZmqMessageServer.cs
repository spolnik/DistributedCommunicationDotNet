using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Extensions;
using NProg.Distributed.Service.Messaging;
using ZeroMQ;

namespace NProg.Distributed.ZeroMQ
{
    /// <summary>
    /// Class ZmqMessageServer.
    /// </summary>
    public sealed class ZmqMessageServer : IRunnable
    {
        /// <summary>
        /// The port
        /// </summary>
        private readonly int port;

        /// <summary>
        /// The message receiver
        /// </summary>
        private readonly IMessageReceiver messageReceiver;
        /// <summary>
        /// The context
        /// </summary>
        private ZmqContext context;
        /// <summary>
        /// The response queue
        /// </summary>
        private ZmqResponseQueue responseQueue;
        /// <summary>
        /// The token
        /// </summary>
        private readonly CancellationTokenSource token;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmqMessageServer"/> class.
        /// </summary>
        /// <param name="messageReceiver">The message receiver.</param>
        /// <param name="port">The port.</param>
        public ZmqMessageServer(IMessageReceiver messageReceiver, int port)
        {
            token = new CancellationTokenSource();

            this.port = port;
            this.messageReceiver = messageReceiver;
            context = ZmqContext.Create();
            responseQueue = new ZmqResponseQueue(context, port);
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            Task.Run(() => StartListening());
        }

        /// <summary>
        /// Starts the listening.
        /// </summary>
        private void StartListening()
        {
            Console.WriteLine("Listening on: tcp://127.0.0.1:{0}", port);
            responseQueue.Listen(x =>
            {
                var message = messageReceiver.Send(x);
                responseQueue.Response(message);
            }, token);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposing && !token.IsCancellationRequested)
            {
                token.Cancel();
            }

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

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Class ZmqResponseQueue.
        /// </summary>
        private sealed class ZmqResponseQueue : IDisposable
        {
            /// <summary>
            /// The socket
            /// </summary>
            private readonly ZmqSocket socket;

            /// <summary>
            /// Initializes a new instance of the <see cref="ZmqResponseQueue"/> class.
            /// </summary>
            /// <param name="context">The context.</param>
            /// <param name="port">The port.</param>
            public ZmqResponseQueue(ZmqContext context, int port)
            {
                socket = context.CreateSocket(SocketType.REP);
                var address = string.Format("tcp://127.0.0.1:{0}", port);
                socket.Bind(address);
            }

            /// <summary>
            /// Responses the specified message.
            /// </summary>
            /// <param name="message">The message.</param>
            public void Response(Message message)
            {
                var json = message.ToJsonString();
                socket.Send(json, Encoding.UTF8);
            }

            /// <summary>
            /// Listens the specified on message received.
            /// </summary>
            /// <param name="onMessageReceived">The on message received.</param>
            /// <param name="token">The token.</param>
            public void Listen(Action<Message> onMessageReceived, CancellationTokenSource token)
            {
                socket.ReceiveReady += (sender, args) =>
                {
                    var inbound = socket.Receive(Encoding.UTF8);

                    var message = Message.FromJson(inbound);
                    onMessageReceived(message);
                };

                var poller = new Poller(new List<ZmqSocket> { socket });

                while (!token.IsCancellationRequested)
                {
                    poller.Poll();
                }
            }

            /// <summary>
            /// Releases unmanaged and - optionally - managed resources.
            /// </summary>
            /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
            private void Dispose(bool disposing)
            {
                if (disposing && socket != null)
                {
                    socket.Dispose();
                }
            }

            /// <summary>
            /// Disposes this instance.
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

    }
}