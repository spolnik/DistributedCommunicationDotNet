using System;
using Ninject;
using Ninject.Modules;
using Ninject.Extensions.Conventions;
using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Domain.Api;
using NProg.Distributed.CarRental.Service;
using NProg.Distributed.CarRental.Service.Business;
using NProg.Distributed.CarRental.Service.Handlers;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.Transport.Ice;
using NProg.Distributed.Transport.NetMQ;
using NProg.Distributed.Transport.Thrift;
using NProg.Distributed.Transport.WCF;
using NProg.Distributed.Transport.ZeroMQ;
using NProg.Distributed.Transport.Zyan;

namespace NProg.Distributed.CarRental.Bootstrapper
{
    public class CarRentalServiceModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            TransportLayer();

            DbLayer();

            MessageHandlers();

            Server();

            Client();
        }

        private void Client()
        {
            Bind<IInventoryApi>().ToMethod<IInventoryApi>(x =>
            {
                var framework = Kernel.Settings.Get("framework", string.Empty);
                var serviceUri = Kernel.Settings.Get("serviceUri", default(Uri));

                var orderServiceFactory = Kernel.Get<IServiceFactory>(framework);
                var requestSender = orderServiceFactory.GetRequestSender(serviceUri);

                return new InventoryClient(requestSender);
            });
        }

        private void Server()
        {
            Bind<IMessageReceiver>().To<MessageReceiver>();
            
            Bind<IServer>().ToMethod<IServer>(x =>
            {
                var framework = Kernel.Settings.Get("framework", string.Empty);
                var port = Kernel.Settings.Get("port", -1);

                var serviceFactory = Kernel.Get<IServiceFactory>(framework);
                var messageReceiver = Kernel.Get<IMessageReceiver>();

                return serviceFactory.GetServer(messageReceiver, port);
            });
        }

        private void MessageHandlers()
        {
            Bind<ICarRentalEngine>().To<CarRentalEngine>();

            Kernel.Bind(x => x.FromAssemblyContaining<AddOrUpdateCarHandler>()
                .SelectAllTypes().InheritedFrom<IMessageHandler>()
                .BindAllInterfaces());
        }

        private void DbLayer()
        {
            Bind<IAccountRepository>().To<AccountRepository>();
            Bind<ICarRepository>().To<CarRepository>();
            Bind<IRentalRepository>().To<RentalRepository>();
            Bind<IReservationRepository>().To<ReservationRepository>();
        }

        private void TransportLayer()
        {
            Bind<IServiceFactory>().To<IceServiceFactory>().Named("ice");
            Bind<IServiceFactory>().To<WcfServiceFactory>().Named("wcf");
            Bind<IServiceFactory>().To<ZmqServiceFactory>().Named("zmq");
            Bind<IServiceFactory>().To<NmqServiceFactory>().Named("nmq");
            Bind<IServiceFactory>().To<ZyanServiceFactory>().Named("zyan");
            Bind<IServiceFactory>().To<ThriftServiceFactory>().Named("thrift");
        }

        #endregion
    }
}