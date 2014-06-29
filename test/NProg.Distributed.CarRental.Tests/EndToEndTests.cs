using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Ninject;
using NProg.Distributed.CarRental.Bootstrapper;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.CarRental.Domain.Api;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;
using NUnit.Framework;

namespace NProg.Distributed.CarRental.Tests
{
    public class EndToEndTests
    {

        private static readonly object[] ServicesCases =
        {
            new object[] {"ice", 49001, 100},
            new object[] {"wcf", 43001, 100},
            new object[] {"thrift", 44001, 100},
            new object[] {"zmq", 45001, 100},
            new object[] {"nmq", 46001, 100},
            new object[] {"zyan", 48001, 100}
        };

        [Test, TestCaseSource("ServicesCases")]
        public void RunServiceTestCase(string framework, int port, int count)
        {
            var kernel = new StandardKernel(new CarRentalServiceModule());
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

            var kernel = new StandardKernel(new CarRentalServiceModule());
            kernel.Settings.Set("framework", framework);
            kernel.Settings.Set("serviceUri", new Uri("tcp://127.0.0.1:" + port));

            using (var client = kernel.Get<IInventoryApi>())
            {
                for (var i = 0; i < count; i++)
                {
                    ProcessCar(client, i);
                }
            }

            stopwatch.Stop();

            Log.WriteLine("===============\nCount: {0}, elapsed: {1} ms\n=============== ", count,
                stopwatch.ElapsedMilliseconds);
        }

        private static void ProcessCar(IInventoryApi client, int i)
        {
            var car = new Car
            {
                Color = Color.Red.ToString(),
                Description = "Car #" + i,
                RentalPrice = 100.0m * (i + 1),
                Year = Convert.ToInt32(Math.Min(1997.0 + i, 2014))
            };

            var carId = client.UpdateCar(car).CarId;
            car.CarId = carId;

            var carFromDb = client.GetCar(carId);
            Debug.Assert(carFromDb.Equals(car));

            client.DeleteCar(carId);

            try
            {
                client.GetCar(carId);
            }
            catch (MessageException messageException)
            {
                Console.WriteLine(messageException.Message);
            }
            
            Log.WriteLine("Car {0} with carId: {1}", i, carId);
        }
    }
}