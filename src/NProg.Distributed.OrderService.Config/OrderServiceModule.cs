﻿using System;
using Ninject;
using Ninject.Modules;
using NProg.Distributed.Ice;
using NProg.Distributed.NetMQ;
using NProg.Distributed.OrderService.Api;
using NProg.Distributed.OrderService.Database;
using NProg.Distributed.OrderService.Handlers;
using NProg.Distributed.Remoting;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;
using NProg.Distributed.Thrift;
using NProg.Distributed.WCF;
using NProg.Distributed.ZeroMQ;
using NProg.Distributed.Zyan;

namespace NProg.Distributed.OrderService.Config
{
    public class OrderServiceModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            // Transport layer
            Bind<IServiceFactory>().To<IceServiceFactory>().Named("ice");
            Bind<IServiceFactory>().To<WcfServiceFactory>().Named("wcf");
            Bind<IServiceFactory>().To<RemotingServiceFactory>().Named("remoting");
            Bind<IServiceFactory>().To<ZmqServiceFactory>().Named("zmq");
            Bind<IServiceFactory>().To<NmqServiceFactory>().Named("nmq");
            Bind<IServiceFactory>().To<ZyanServiceFactory>().Named("zyan");
            Bind<IServiceFactory>().To<ThriftServiceFactory>().Named("thrift");

            // DB Layer
            Bind<IOrderApi>().To<InMemoryDao>();
//            Bind<IOrderApi>().To<NdbOrderDao>();

            // Message handers
            Bind<IMessageHandler>().To<AddOrderHandler>();
            Bind<IMessageHandler>().To<GetOrderHandler>();
            Bind<IMessageHandler>().To<RemoveOrderHandler>();

            // Message layer integration
            Bind<IMessageReceiver>().To<MessageReceiver>();

            // Set up server
            Bind<IServer>().ToMethod<IServer>(x =>
                {
                    var framework = Kernel.Settings.Get("framework", string.Empty);
                    var port = Kernel.Settings.Get("port", -1);

                    var serviceFactory = Kernel.Get<IServiceFactory>(framework);
                    var messageReceiver = Kernel.Get<IMessageReceiver>();
                    var messageMapper = serviceFactory.GetMessageMapper();

                    return serviceFactory.GetServer(messageReceiver, messageMapper, port);
                });

            // Set up client
            Bind<IOrderClient>().ToMethod<IOrderClient>(x =>
                {
                    var framework = Kernel.Settings.Get("framework", string.Empty);
                    var serviceUri = Kernel.Settings.Get("serviceUri", default(Uri));

                    var orderServiceFactory = Kernel.Get<IServiceFactory>(framework);
                    var messageMapper = orderServiceFactory.GetMessageMapper();
                    var requestSender = orderServiceFactory.GetRequestSender(serviceUri, messageMapper);

                    return new OrderClient(requestSender);
                });
        }

        #endregion
    }
}