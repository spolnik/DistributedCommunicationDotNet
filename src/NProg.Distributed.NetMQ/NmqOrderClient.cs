﻿using System;
using System.Collections.Generic;
using NetMQ;
using NProg.Distributed.Domain;
using NProg.Distributed.Messaging.Queries;
using NProg.Distributed.Messaging.Spec;
using NProg.Distributed.NetMQ.Messaging;
using NProg.Distributed.Service;

namespace NProg.Distributed.NetMQ
{
    public class NmqOrderClient : IHandler<Order>, IDisposable
    {
       private readonly NetMQContext context;

       public NmqOrderClient()
        {
            context = NetMQContext.Create();
        }

        public void Add(Order item)
        {
            var messageQueue = MessageQueueFactory.CreateOutbound(AddOrderRequest.Name, MessagePattern.RequestResponse, GetProperties());
            messageQueue.Send(new Message
            {
                Body = new AddOrderRequest {Order = item}
            });

            var responseQueue = messageQueue.GetResponseQueue();

            responseQueue.Receive(x => Console.WriteLine("Order added: {0}", x.BodyAs<StatusResponse>().Status));
        }

        public Order Get(Guid guid)
        {
            var messageQueue = MessageQueueFactory.CreateOutbound(GetOrderRequest.Name, MessagePattern.RequestResponse, GetProperties());
            messageQueue.Send(new Message
            {
                Body = new GetOrderRequest{ OrderId = guid }
            });

            var responseQueue = messageQueue.GetResponseQueue();

            Order order = null;
            responseQueue.Receive(x =>
            {
                order = x.BodyAs<GetOrderResponse>().Order;
            });

            return order;
        }

        public bool Remove(Guid guid)
        {
            var messageQueue = MessageQueueFactory.CreateOutbound(RemoveOrderRequest.Name, MessagePattern.RequestResponse, GetProperties());
            messageQueue.Send(new Message
            {
                Body = new RemoveOrderRequest { OrderId = guid }
            });

            var responseQueue = messageQueue.GetResponseQueue();
            
            var status = false;
            responseQueue.Receive(x =>
            {
                status = x.BodyAs<StatusResponse>().Status;
            });

            return status;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && context != null)
                context.Dispose();
        }

        private Dictionary<string, object> GetProperties()
        {
            return new Dictionary<string, Object> { { "context", context } };
        }
    }
}