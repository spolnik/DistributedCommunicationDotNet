using System;
using Ninject;
using Ninject.Modules;
using NProg.Distributed.Core.Data;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.Database.InMemory;
using NProg.Distributed.OrderService.Domain;
using NProg.Distributed.OrderService.Domain.Api;
using NProg.Distributed.OrderService.Handlers;
using NProg.Distributed.OrderService.Remoting;
using NProg.Distributed.Transport.Ice;
using NProg.Distributed.Transport.NetMQ;
using NProg.Distributed.Transport.Thrift;
using NProg.Distributed.Transport.WCF;
using NProg.Distributed.Transport.ZeroMQ;
using NProg.Distributed.Transport.Zyan;

namespace NProg.Distributed.OrderService.Bootstrapper
{
    public sealed class OrderServiceModule : NinjectModule
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
            Bind<IOrderApi>().ToMethod<IOrderApi>(x =>
                {
                    var framework = Kernel.Settings.Get("framework", string.Empty);
                    var serviceUri = Kernel.Settings.Get("serviceUri", default(Uri));

                    var orderServiceFactory = Kernel.Get<IServiceFactory>(framework);
                    var requestSender = orderServiceFactory.GetRequestSender(serviceUri);

                    return new OrderClient(requestSender);
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
            Bind<IMessageHandler>().To<AddOrderHandler>();
            Bind<IMessageHandler>().To<GetOrderHandler>();
            Bind<IMessageHandler>().To<RemoveOrderHandler>();
        }

        private void DbLayer()
        {
            Bind<IDataRepository<Guid, Order>>().To<InMemoryRepository<Guid, Order>>();
//            Bind<IDataRepository<Guid, Order>>().To<NDatabaseRepository<Guid, Order>>();
        }

        private void TransportLayer()
        {
            Bind<IServiceFactory>().To<IceServiceFactory>().Named("ice");
            Bind<IServiceFactory>().To<WcfServiceFactory>().Named("wcf");
            Bind<IServiceFactory>().To<RemotingServiceFactory>().Named("remoting");
            Bind<IServiceFactory>().To<ZmqServiceFactory>().Named("zmq");
            Bind<IServiceFactory>().To<NmqServiceFactory>().Named("nmq");
            Bind<IServiceFactory>().To<ZyanServiceFactory>().Named("zyan");
            Bind<IServiceFactory>().To<ThriftServiceFactory>().Named("thrift");
        }

        #endregion
    }
}