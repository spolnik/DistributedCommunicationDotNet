using System;
using System.Diagnostics;
using System.Drawing;
using log4net;
using log4net.Config;
using Ninject;
using NProg.Distributed.CarRental.Bootstrapper;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.CarRental.Domain.Api;

namespace NProg.Distributed.CarRental.Client
{
    class ClientProgram
    {
        internal static void Main(string[] args)
        {
            Console.WriteLine("Press to run ...");
            Console.ReadLine();

            if (args.Length < 3)
            {
                throw new ArgumentException("Usage: NProg.Distributed.CarRental.Client.exe <framework> <port> <count>");
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

        private static void SetLogProperties(string framework, int count)
        {
            GlobalContext.Properties["framework"] = framework;
            GlobalContext.Properties["count"] = count;
        }

        private static void ProcessCar(IInventoryApi client, int i)
        {
            var car = new Car
            {
                Color = Color.Red.ToString(),
                Description = "Car #" + i,
                RentalPrice = 100.0m * (i+1),
                Year = Convert.ToInt32(Math.Min(1997.0 + i, 2014))
            };

            var carId = client.UpdateCar(car).CarId;
            car.CarId = carId;

            var carFromDb = client.GetCar(carId);
            Debug.Assert(carFromDb.Equals(car));

            client.DeleteCar(carId);

            var removedCar = client.GetCar(carId);
            Debug.Assert(removedCar.Equals(new Car()));

            Log.WriteLine("Car {0} with carId: {1}", i, carId);
        }

        #region Nested type: Log

        private static class Log
        {
            private static readonly ILog Logger;

            static Log()
            {
                XmlConfigurator.Configure();
                Logger = LogManager.GetLogger(typeof(ClientProgram));
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
