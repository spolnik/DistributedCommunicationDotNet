using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using NProg.Distributed.Domain;
using NProg.Distributed.Domain.Handlers;
using NProg.Distributed.Ice;
using NProg.Distributed.NDatabase;
using NProg.Distributed.NetMQ;
using NProg.Distributed.Remoting;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;
using NProg.Distributed.Thrift;
using NProg.Distributed.WCF;
using NProg.Distributed.ZeroMQ;
using NProg.Distributed.Zyan;
using NUnit.Framework;

namespace NProg.Distributed.Tests
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
//            new object[] {"remoting", 37001, 100},
        };

        [Test, TestCaseSource("ServicesCases")]
        public void RunServiceTestCase(string framework, int port, int count)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(() => RunServer(framework, port, cancellationTokenSource), cancellationTokenSource.Token);
            RunClient(framework, port, count);
            cancellationTokenSource.Cancel(false);
        }

        private static void RunServer(string framework, int port, CancellationTokenSource source)
        {
            Log.WriteLine("Running for framework: {0}, port: {1}", framework, port);

            IRunnable server = null;

            try
            {
                var orderServiceFactory = GetOrderServiceFactory(framework);
                var messageMapper = orderServiceFactory.GetMessageMapper();

                var inMemoryDao = new InMemoryDao();
                var register = new List<IMessageHandler>
                {
                    new AddOrderHandler(inMemoryDao),
                    new GetOrderHandler(inMemoryDao),
                    new RemoveOrderHandler(inMemoryDao)
                };

                var handlerRegister = new HandlerRegister(register);
                var messageReceiver = new MessageReceiver(handlerRegister);

                server = orderServiceFactory.GetServer(messageReceiver, messageMapper, port);

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

            var orderServiceFactory = GetOrderServiceFactory(framework);
            var messageMapper = orderServiceFactory.GetMessageMapper();
            var requestSender = orderServiceFactory.GetRequestSender(new Uri("tcp://127.0.0.1:" + port), messageMapper);
            using (var client = new OrderClient(requestSender))
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

            Log.WriteLine("===============\nCount: {0}, ellapsed: {1} ms\n=============== ", count,
                stopwatch.ElapsedMilliseconds);
        }

        private static IServiceFactory GetOrderServiceFactory(string framework)
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