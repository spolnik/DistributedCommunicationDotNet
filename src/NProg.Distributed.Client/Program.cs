using System;
using System.Diagnostics;
using log4net;
using NProg.Distributed.Ice;
using NProg.Distributed.NetMQ;
using NProg.Distributed.Remoting;
using NProg.Distributed.Service;
using NProg.Distributed.Thrift;
using NProg.Distributed.WCF;
using NProg.Distributed.ZeroMQ;
using NProg.Distributed.Zyan;

namespace NProg.Distributed.Client
{
    public static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
                throw new ArgumentException("Usage: NProg.Distributed.Client.exe <framework> <port> <count>");

            var framework = args[0];
            GlobalContext.Properties["framework"] = framework;

            var port = Convert.ToInt32(args[1]);
            var count = Convert.ToInt32(args[2]);
            GlobalContext.Properties["count"] = count;

            var stopwatch = new Stopwatch();
            
            try
            {
                Log.WriteLine("Running for framework: {0}, request count: {1}, port: {2}", framework, count, port);
                stopwatch.Start();

                var orderServiceFactory = GetOrderServiceFactory(framework);
                var messageMapper = orderServiceFactory.GetMessageMapper();
                var client = orderServiceFactory.GetClient(new Uri("tcp://127.0.0.1:" + port), messageMapper);

                try
                {
                    for (var i = 0; i < count; i++)
                    {
                        var order = new Domain.Order
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
                        Debug.Assert(removedOrder.Equals(new Domain.Order{UserName = ""}));

                        Log.WriteLine("Order {0}", i);
                    }
                }
                finally
                {
                    var disposable = client as IDisposable;

                    if (disposable != null)
                        disposable.Dispose();
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
            stopwatch.Stop();

            Log.WriteLine("===============\nCount: {0}, ellapsed: {1} ms\n=============== ", count, stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Press <enter> to close client...");
            Console.ReadLine();
        }

        private static IServiceFactory<Guid, Domain.Order> GetOrderServiceFactory(string framework)
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
