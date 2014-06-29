using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Ninject;
using NProg.Distributed.Core.Service;
using NProg.Distributed.OrderService.Api;
using NProg.Distributed.OrderService.Config;
using NProg.Distributed.OrderService.Domain;
using NUnit.Framework;

namespace NProg.Distributed.Transport.Tests
{
    public class EndToEndTests
    {
        private static readonly object[] ServicesCases =
        {
            new object[] {"wcf", 33001, 100},
            new object[] {"thrift", 34001, 100},
            new object[] {"zmq", 35001, 100},
            new object[] {"nmq", 36001, 100},
            new object[] {"zyan", 38001, 100},
            new object[] {"ice", 39001, 100}
        };

        [Test, TestCaseSource("ServicesCases")]
        public void RunServiceTestCase(string framework, int port, int count)
        {
            var kernel = new StandardKernel(new OrderServiceModule());
            kernel.Settings.Set("framework", framework);
            kernel.Settings.Set("port", port);

            var cancellationTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(() => RunServer(kernel, port, cancellationTokenSource), cancellationTokenSource.Token);
            RunClient(framework, port, count);
            cancellationTokenSource.Cancel(true);
        }

        private static void RunServer(IKernel kernel, int port, CancellationTokenSource source)
        {
            Log.WriteLine("Running for framework: {0}, port: {1}", kernel.Settings.Get("framework", string.Empty), port);

            IServer server = null;

            try
            {
                server = kernel.Get<IServer>();

                Log.WriteLine("Server running ...");
                server.Run();

                var delay = Task.Delay(TimeSpan.FromMinutes(5));
                delay.Wait(source.Token);
            }
            catch (OperationCanceledException exception)
            {
                Log.WriteLine(exception.Message);
            }
            finally
            {
                if (server != null)
                    server.Dispose();

                Log.WriteLine("Server stopped.");
            }
        }

        private static void RunClient(string framework, int port, int count)
        {
            GlobalContext.Properties["framework"] = framework;
            GlobalContext.Properties["count"] = count;

            var stopwatch = new Stopwatch();


            Log.WriteLine("Running for framework: {0}, request count: {1}, port: {2}", framework, count, port);
            stopwatch.Start();

            var kernel = new StandardKernel(new OrderServiceModule());
            kernel.Settings.Set("framework", framework);
            kernel.Settings.Set("serviceUri", new Uri("tcp://127.0.0.1:" + port));

            using (var client = kernel.Get<IOrderApi>())
            {
                for (var i = 0; i < count; i++)
                {
                    var order = new Order
                    {
                        Count = 3,
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
                    Debug.Assert(removedOrder.Equals(new Order { UserName = "" }));

                    Log.WriteLine("Order {0}", i);
                }
            }

            stopwatch.Stop();

            Log.WriteLine("===============\nCount: {0}, elapsed: {1} ms\n=============== ", count,
                stopwatch.ElapsedMilliseconds);
        }
    }
}