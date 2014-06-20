﻿using System;
using System.Diagnostics;
using log4net;
using NProg.Distributed.Ice;
using NProg.Distributed.NetMQ;
using NProg.Distributed.OrderService;
using NProg.Distributed.OrderService.Api;
using NProg.Distributed.OrderService.Domain;
using NProg.Distributed.Remoting;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;
using NProg.Distributed.Thrift;
using NProg.Distributed.WCF;
using NProg.Distributed.ZeroMQ;
using NProg.Distributed.Zyan;

namespace NProg.Distributed.Client
{
    public static class ClientProgram
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <exception cref="System.ArgumentException">Usage: NProg.Distributed.Client.exe framework port count</exception>
        public static void Main(string[] args)
        {
            if (args.Length < 3)
                throw new ArgumentException("Usage: NProg.Distributed.Client.exe <framework> <port> <count>");

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

            Log.WriteLine("===============\nCount: {0}, elapsed: {1} ms\n=============== ", count, stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Press <enter> to close client...");
            Console.ReadLine();
        }

        private static IRequestSender GetRequestSender(string framework, int port)
        {
            var orderServiceFactory = GetOrderServiceFactory(framework);
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