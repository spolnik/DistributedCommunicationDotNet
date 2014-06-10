using System;
using NProg.Distributed.Service;
using NProg.Distributed.Thrift;

namespace NProg.Distributed.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IServer server = null;

            try
            {
                const int port = 55001;

                IOrderServiceFactory orderServiceFactory = new ThriftOrderServiceFactory();
                var ordersHandler = orderServiceFactory.GetHandler();

                server = orderServiceFactory.GetServer(ordersHandler, port);
                Console.WriteLine("Server running on: tcp://localhost:{0}", port);
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
