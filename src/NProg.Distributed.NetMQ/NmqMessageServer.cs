using System;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Extensions;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.NetMQ
{
    /// <summary>
    /// Class NmqMessageServer.
    /// </summary>
    internal sealed class NmqMessageServer : IRunnable
    {
        /// <summary>
        /// The message receiver
        /// </summary>
        private readonly IMessageReceiver messageReceiver;

        /// <summary>
        /// The port
        /// </summary>
        private readonly int port;

        /// <summary>
        /// The token
        /// </summary>
        private readonly CancellationTokenSource token;

        /// <summary>
        /// The context
        /// </summary>
        private NetMQContext context;

        /// <summary>
        /// The response queue
        /// </summary>
        private NmqResponseQueue responseQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="NmqMessageServer"/> class.
        /// </summary>
        /// <param name="messageReceiver">The message receiver.</param>
        /// <param name="port">The port.</param>
        internal NmqMessageServer(IMessageReceiver messageReceiver, int port)
        {
            token = new CancellationTokenSource();

            this.port = port;
            this.messageReceiver = messageReceiver;
            context = NetMQContext.Create();
            responseQueue = new NmqResponseQueue(context, port);
        }

        #region IRunnable Members

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            Task.Run(() => StartListening());
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

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
        protected void Dispose(bool disposing)
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

        #region Nested type: NmqResponseQueue

        /// <summary>
        /// Class NmqResponseQueue.
        /// </summary>
        private sealed class NmqResponseQueue : IDisposable
        {
            /// <summary>
            /// The socket
            /// </summary>
            private NetMQSocket socket;

            /// <summary>
            /// Initializes a new instance of the <see cref="NmqResponseQueue"/> class.
            /// </summary>
            /// <param name="context">The context.</param>
            /// <param name="port">The port.</param>
            internal NmqResponseQueue(NetMQContext context, int port)
            {
                socket = context.CreateResponseSocket();
                var address = string.Format("tcp://127.0.0.1:{0}", port);
                socket.Bind(address);
            }

            #region IDisposable Members

            /// <summary>
            /// Disposes this instance.
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            /// <summary>
            /// Responses the specified message.
            /// </summary>
            /// <param name="message">The message.</param>
            internal void Response(Message message)
            {
                var json = message.ToJsonString();
                socket.Send(json);
            }

            /// <summary>
            /// Listens the specified on message received.
            /// </summary>
            /// <param name="onMessageReceived">The on message received.</param>
            /// <param name="token">The token.</param>
            internal void Listen(Action<Message> onMessageReceived, CancellationTokenSource token)
            {
                socket.ReceiveReady += (sender, args) =>
                    {
                        var inbound = socket.ReceiveString();

                        var message = Message.FromJson(inbound);
                        onMessageReceived(message);
                    };

                while (!token.IsCancellationRequested)
                {
                    socket.Poll();
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
                    socket = null;
                }
            }

        }

        #endregion
    }
}