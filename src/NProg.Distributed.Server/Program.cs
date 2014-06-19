using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Ice;
using NProg.Distributed.NetMQ;
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
        static void Main(string[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException("Usage: NProg.Distributed.Client.exe <framework> <port>");

            var framework = args[0];
            var port = Convert.ToInt32(args[1]);
            Console.WriteLine("Running for framework: {0}, port: {1}", framework, port);

            IServer server = null;
            
            try
            {
                var orderServiceFactory = GetOrderServiceFactory(framework);
                var messageMapper = orderServiceFactory.GetMessageMapper();
                var ordersHandler = orderServiceFactory.GetHandler(messageMapper);

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

        private static IOrderServiceFactory<Guid, Order> GetOrderServiceFactory(string framework)
        {
            switch (framework)
            {
                case "wcf":
                    return new WcfOrderServiceFactory();
                case "thrift":
                    return new ThriftOrderServiceFactory();
                case "zmq":
                    return new ZmqOrderServiceFactory();
                case "nmq":
                    return new NmqOrderServiceFactory();
                case "remoting":
                    return new RemotingOrderServiceFactory();
                case "zyan":
                    return new ZyanOrderServiceFactory();
                case "ice":
                    return new IceOrderServiceFactory();
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
