using System;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using log4net;
using log4net.Config;
using NProg.Distributed.OrderService;
using NProg.Distributed.OrderService.Api;
using NProg.Distributed.OrderService.Domain;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Composition;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Client
{
    internal static class ClientProgram
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <exception cref="System.ArgumentException">Usage: NProg.Distributed.Client.exe framework port count</exception>
        internal static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                throw new ArgumentException("Usage: NProg.Distributed.Client.exe <framework> <port> <count>");
            }

            var framework = args[0];
            var port = Convert.ToInt32(args[1]);
            var count = Convert.ToInt32(args[2]);

            SetLogProperties(framework, count);

            var stopwatch = new Stopwatch();

            try
            {
                Log.WriteLine("Running for framework: {0}, request count: {1}, port: {2}", framework, count, port);
                stopwatch.Start();

                var requestSender = GetRequestSender(framework, port);

                using (var client = new OrderClient(requestSender))
                {
                    for (var i = 0; i < count; i++)
                    {
                        ProcessOrder(client, i);
                    }
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
            stopwatch.Stop();

            Log.WriteLine("===============\nCount: {0}, elapsed: {1} ms\n=============== ", count,
                stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Press <enter> to close client...");
            Console.ReadLine();
        }

        private static IRequestSender GetRequestSender(string framework, int port)
        {
            var orderServiceFactory = GetServiceFactory(framework);
            var messageMapper = orderServiceFactory.GetMessageMapper();
            var requestSender = orderServiceFactory.GetRequestSender(new Uri("tcp://127.0.0.1:" + port),
                messageMapper);

            return requestSender;
        }

        private static void SetLogProperties(string framework, int count)
        {
            GlobalContext.Properties["framework"] = framework;
            GlobalContext.Properties["count"] = count;
        }

        private static void ProcessOrder(IOrderApi client, int i)
        {
            var order = new Order
                {
                    Count = i,
                    OrderDate = DateTime.Now,
                    OrderId = Guid.NewGuid(),
                    UnitPrice = 12.23m,
                    UserName = "Mikolaj"
                };

            client.Add(order.OrderId, order);

            var orderFromDb = client.Get(order.OrderId);
            Debug.Assert(orderFromDb.Equals(order));

            var removed = client.Remove(order.OrderId);
            Debug.Assert(removed);

            var removedOrder = client.Get(order.OrderId);
            removedOrder.UserName = "";
            Debug.Assert(removedOrder.Equals(new Order {UserName = ""}));

            Log.WriteLine("Order {0}", i);
        }

        private static IServiceFactory GetServiceFactory(string framework)
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

        #region Nested type: Log

        private static class Log
        {
            private static readonly ILog Logger;

            static Log()
            {
                XmlConfigurator.Configure();
                Logger = LogManager.GetLogger(typeof (ClientProgram));
            }

            internal static void WriteLine(string format, params object[] args)
            {
                Logger.Debug(string.Format(format, args));
            }

            internal static void Error(Exception exception)
            {
                Logger.Error(exception.Message, exception);
            }
        }

        #endregion
    }
}