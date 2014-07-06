using System;
using Ninject;
using NProg.Distributed.Core.Service;
using NProg.Distributed.OrderService.Bootstrapper;

namespace NProg.Distributed.Server
{
    internal static class ServerProgram
    {
        internal static void Main(string[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException("Usage: NProg.Distributed.Server.exe <framework> <port>");

            var framework = args[0];
            var port = Convert.ToInt32(args[1]);
            Console.WriteLine("Running for framework: {0}, port: {1}", framework, port);

            var kernel = new StandardKernel(new OrderServiceModule());
            kernel.Settings.Set("framework", framework);
            kernel.Settings.Set("port", port);

            using (var server = kernel.Get<IServer>())
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
