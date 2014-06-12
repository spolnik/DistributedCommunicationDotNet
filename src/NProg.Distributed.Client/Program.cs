using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;
using NProg.Distributed.Thrift;
using NProg.Distributed.ZeroMQ;
using Order = NProg.Distributed.Domain.Order;

namespace NProg.Distributed.Client
{
    class Program
    {
        static void Main(string[] args)
        {
//            IOrderServiceFactory orderServiceFactory = new ThriftOrderServiceFactory();
            IOrderServiceFactory orderServiceFactory = new ZmqOrderServiceFactory();
            var client = orderServiceFactory.GetClient(new Uri("tcp://127.0.0.1:55001"));

            for (var i = 0; i < 1000; i++)
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
                Console.WriteLine("Order added, id: {0}", order.OrderId);
                Console.WriteLine("===================");


                var orderFromDb = client.Get(order.OrderId);
                Console.WriteLine("Order from DB: {0}", orderFromDb);
                Console.WriteLine("===================");

                var removed = client.Remove(order.OrderId);
                Console.WriteLine("Order removed: {0}", removed);
                Console.WriteLine("===================");

                var removedOrder = client.Get(order.OrderId);
                Console.WriteLine("Removed Order from DB: {0}", removedOrder);
                Console.WriteLine("===================");
            }
        }
    }
}
