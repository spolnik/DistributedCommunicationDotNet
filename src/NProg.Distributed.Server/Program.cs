using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Msmq;
using NProg.Distributed.Remoting;
using NProg.Distributed.Service;
using NProg.Distributed.Thrift;
using NProg.Distributed.WCF;
using NProg.Distributed.ZeroMQ;
using NProg.Distributed.Zyan;

namespace NProg.Distributed.Server
{
    public static class Program
    {
        static void Main()
        {
            IServer server = null;
            
            try
            {
                const int port = 55001;

                var orderServiceFactory = GetOrderServiceFactory("zyan");
                var ordersHandler = orderServiceFactory.GetHandler();

                server = orderServiceFactory.GetServer(ordersHandler, port);
                
                Console.WriteLine("Server running ...");
                server.Start();
                Console.WriteLine("Press <enter> to stop server...");
                Console.ReadLine();
            }
            finally
            {
                if (server != null)
                    server.Stop();

                Console.WriteLine("Server stopped.");    
            }
        }

        private static IServiceFactory<Order> GetOrderServiceFactory(string framework)
        {
            switch (framework)
            {
                case "wcf":
                    return new WcfOrderServiceFactory();
                case "thrift":
                    return new ThriftOrderServiceFactory();
                case "zmq":
                    return new ZmqOrderServiceFactory();
                case "msmq":
                    return new MsmqOrderServiceFactory();
                case "remoting":
                    return new RemotingOrderServiceFactory();
                case "zyan":
                    return new ZyanOrderServiceFactory();
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
