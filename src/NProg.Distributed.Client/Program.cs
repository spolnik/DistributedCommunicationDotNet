using System;
using NProg.Distributed.Msmq;
using NProg.Distributed.NetMQ;
using NProg.Distributed.Remoting;
using NProg.Distributed.Service;
using NProg.Distributed.Thrift;
using NProg.Distributed.WCF;
using NProg.Distributed.ZeroMQ;
using NProg.Distributed.Zyan;
using Order = NProg.Distributed.Domain.Order;

namespace NProg.Distributed.Client
{
    public static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
                throw new ArgumentException("Usage: NProg.Distributed.Client.exe <framework> <port> <count>");

            var framework = args[0];
            var port = Convert.ToInt32(args[1]);
            var count = Convert.ToInt32(args[2]);
            Log.WriteLine("Running for framework: {0}, request count: {1}, port: {2}", framework, count, port);

            var orderServiceFactory = GetOrderServiceFactory(framework);
            var client = orderServiceFactory.GetClient(new Uri("tcp://127.0.0.1:" + port));

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

                client.Add(order);
                Log.WriteLine("Order added, id: {0}", order.OrderId);
                Log.WriteLine("===================");


                var orderFromDb = client.Get(order.OrderId);
                Log.WriteLine("Order from DB: {0}", orderFromDb);
                Log.WriteLine("===================");

                var removed = client.Remove(order.OrderId);
                Log.WriteLine("Order removed: {0}", removed);
                Log.WriteLine("===================");

                var removedOrder = client.Get(order.OrderId);
                Log.WriteLine("Removed Order from DB: {0}", removedOrder);
                Log.WriteLine("===================");
            }

            Console.WriteLine("Press <enter> to close client...");
            Console.ReadLine();
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
                case "nmq":
                    return new NmqOrderServiceFactory();
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
