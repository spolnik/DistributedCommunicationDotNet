﻿using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Messaging.Order.Queries;
using NProg.Distributed.Messaging.Spec;
using NProg.Distributed.Service;
using NProg.Distributed.ZeroMQ.Messaging;

namespace NProg.Distributed.ZeroMQ
{
    public class ZmqOrderClient : IHandler<Order>
    {   
        public void Add(Order item)
        {
            var messageQueue = MessageQueueFactory.CreateOutbound(AddOrderRequest.Name, MessagePattern.RequestResponse);
            messageQueue.Send(new Message
            {
                Body = new AddOrderRequest {Order = item}
            });

            var responseQueue = messageQueue.GetResponseQueue();

            responseQueue.Receive(x => Console.WriteLine("Order added: {0}", x.BodyAs<StatusResponse>().Status));
        }

        public Order Get(Guid guid)
        {
            var messageQueue = MessageQueueFactory.CreateOutbound(GetOrderRequest.Name, MessagePattern.RequestResponse);
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
            var messageQueue = MessageQueueFactory.CreateOutbound(RemoveOrderRequest.Name, MessagePattern.RequestResponse);
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
    }
}