using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Messaging.Queries;
using NProg.Distributed.Messaging.Spec;
using NProg.Distributed.Msmq.Messaging;
using NProg.Distributed.Service;

namespace NProg.Distributed.Msmq
{
    public class MsmqOrderClient : IHandler<Order>
    {
        public void Add(Order item)
        {
            using (var requestQueue = MessageQueueFactory.CreateOutbound(AddOrderRequest.Name, MessagePattern.RequestResponse))
            {
                using (var responseQueue = requestQueue.GetResponseQueue())
                {
                    requestQueue.Send(new Message
                    {
                        Body = new AddOrderRequest { Order = item },
                        ResponseAddress = responseQueue.Address
                    });

                    responseQueue.Receive(x => Console.WriteLine("Order added: {0}", x.BodyAs<StatusResponse>().Status));
                }
            }
        }

        public Order Get(Guid guid)
        {
            Order order = null;

            using (var requestQueue = MessageQueueFactory.CreateOutbound(GetOrderRequest.Name, MessagePattern.RequestResponse))
            {
                using (var responseQueue = requestQueue.GetResponseQueue())
                {
                    requestQueue.Send(new Message
                    {
                        Body = new GetOrderRequest { OrderId = guid },
                        ResponseAddress = responseQueue.Address
                    });
                    
                    responseQueue.Receive(x =>
                    {
                        order = x.BodyAs<GetOrderResponse>().Order;
                    });
                }
            }

            return order;
        }

        public bool Remove(Guid guid)
        {
            var status = false;

            using (var requestQueue = MessageQueueFactory.CreateOutbound(RemoveOrderRequest.Name, MessagePattern.RequestResponse))
            {
                using (var responseQueue = requestQueue.GetResponseQueue())
                {
                    requestQueue.Send(new Message
                    {
                        Body = new RemoveOrderRequest { OrderId = guid },
                        ResponseAddress = responseQueue.Address
                    });

                    responseQueue.Receive(x =>
                    {
                        status = x.BodyAs<StatusResponse>().Status;
                    });
                }
            }

            return status;
        }
    }
}