using System;
using System.Text;
using NProg.Distributed.Messaging;
using NProg.Distributed.Messaging.Extensions;
using ZMQ;
using Exception = ZMQ.Exception;

namespace NProg.Distributed.ZeroMQ.Messaging
{
    public class ZmqResponseQueue : IMessageQueue
    {
        private readonly Socket socket;

        public ZmqResponseQueue(Context context, int port)
        {
            socket = context.Socket(SocketType.REP);
            var address = string.Format("tcp://127.0.0.1:{0}", port);
            socket.Bind(address);
        }

        public void Send(Message message)
        {
            var json = message.ToJsonString();
            socket.Send(json, Encoding.UTF8);
        }

        public void Listen(Action<Message> onMessageReceived)
        {
            while (true)
            {
                Receive(onMessageReceived);
            }
        }

        public void Receive(Action<Message> onMessageReceived)
        {
            string inbound;

            try
            {
                inbound = socket.Recv(Encoding.UTF8);
            }
            catch (System.Runtime.InteropServices.SEHException)
            {
                Dispose(true);
                return;
            }
            catch (Exception)
            {
                Dispose(true);
                return;
            }

            var message = Message.FromJson(inbound);
            onMessageReceived(message);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing && socket != null)
            {
                socket.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}