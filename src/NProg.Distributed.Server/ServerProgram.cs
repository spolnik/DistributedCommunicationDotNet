using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using NProg.Distributed.OrderService.Handlers;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Composition;
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
            var register = new List<IMessageHandler>
            {
                new AddOrderHandler(),
                new GetOrderHandler(),
                new RemoveOrderHandler()
            };

            var handlerRegister = new HandlerRegister(register);
            var messageReceiver = new MessageReceiver(handlerRegister);
            return messageReceiver;
        }

        private static IServiceFactory GetMessageServiceFactory(string framework)
        {
            var mainDirectoryCatalog = new DirectoryCatalog(".");
            var compositionContainer = new CompositionContainer(mainDirectoryCatalog);
            var export = compositionContainer.GetExport<ServiceComposer>();

            if (export == null)
            {
                return null;
            }

            var serviceComposer = export.Value;
            return serviceComposer.GetFactory(framework);
        }
    }
}
