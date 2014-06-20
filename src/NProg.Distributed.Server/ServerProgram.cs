using System;
using System.Collections.Generic;
using NProg.Distributed.Ice;
using NProg.Distributed.NDatabase;
using NProg.Distributed.NetMQ;
using NProg.Distributed.OrderService.Handlers;
using NProg.Distributed.Remoting;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;
using NProg.Distributed.Thrift;
using NProg.Distributed.WCF;
using NProg.Distributed.ZeroMQ;
using NProg.Distributed.Zyan;

namespace NProg.Distributed.Server
{
    internal static class ServerProgram
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException("Usage: NProg.Distributed.Server.exe <framework> <port>");

            var framework = args[0];
            var port = Convert.ToInt32(args[1]);
            Console.WriteLine("Running for framework: {0}, port: {1}", framework, port);

            var orderServiceFactory = GetMessageServiceFactory(framework);
            var messageMapper = orderServiceFactory.GetMessageMapper();

            var messageReceiver = GetMessageReceiver();

            using (var server = orderServiceFactory.GetServer(messageReceiver, messageMapper, port))
            {
                Console.WriteLine("Server running ...");
                server.Run();
                Console.WriteLine("Press <enter> to stop server...");
                Console.ReadLine();
            }

            Console.WriteLine("Server stopped.");    
        }

        private static MessageReceiver GetMessageReceiver()
        {
            var orderDaoFactory = new InMemoryOrderDaoFactory();

            var register = new List<IMessageHandler>
            {
                new AddOrderHandler(orderDaoFactory),
                new GetOrderHandler(orderDaoFactory),
                new RemoveOrderHandler(orderDaoFactory)
            };

            var handlerRegister = new HandlerRegister(register);
            var messageReceiver = new MessageReceiver(handlerRegister);
            return messageReceiver;
        }

        private static IServiceFactory GetMessageServiceFactory(string framework)
        {
            switch (framework)
            {
                case "wcf":
                    return new WcfServiceFactory();
                case "thrift":
                    return new ThriftServiceFactory();
                case "zmq":
                    return new ZmqServiceFactory();
                case "nmq":
                    return new NmqServiceFactory();
                case "remoting":
                    return new RemotingServiceFactory();
                case "zyan":
                    return new ZyanServiceFactory();
                case "ice":
                    return new IceServiceFactory();
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
