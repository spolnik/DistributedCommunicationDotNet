using System;
using Ninject;
using NProg.Distributed.OrderService.Config;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

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

            var kernel = new StandardKernel(new OrderServiceModule());
            var orderServiceFactory = kernel.Get<IServiceFactory>(framework);
            var messageMapper = orderServiceFactory.GetMessageMapper();

            var messageReceiver = kernel.Get<IMessageReceiver>();

            using (var server = orderServiceFactory.GetServer(messageReceiver, messageMapper, port))
            {
                Console.WriteLine("Server running ...");
                server.Run();
                Console.WriteLine("Press <enter> to stop server...");
                Console.ReadLine();
            }

            Console.WriteLine("Server stopped.");    
        }
    }
}
