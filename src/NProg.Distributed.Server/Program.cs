using System;
using System.Linq;
using NProg.Distributed.Service;
using NProg.Distributed.Thrift;
using NProg.Distributed.ZeroMQ;

namespace NProg.Distributed.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IServer server = null;
            var eventName = string.Empty;

            if (args.Length == 1)
            {
                eventName = args[0];
            }

            try
            {
                const int port = 55001;

//                IOrderServiceFactory orderServiceFactory = new ThriftOrderServiceFactory();
                IOrderServiceFactory orderServiceFactory = new ZmqOrderServiceFactory();
                var ordersHandler = orderServiceFactory.GetHandler();

                server = string.IsNullOrWhiteSpace(eventName)
                    ? orderServiceFactory.GetServer(ordersHandler, port)
                    : orderServiceFactory.GetServer(ordersHandler, eventName: eventName);
                
                Console.WriteLine("Server running ...");
                server.Start();
            }
            finally
            {
                if (server != null)
                    server.Stop();

                Console.WriteLine("Server stopped.");    
            }
            
        }
    }
}
