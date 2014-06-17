using System;
using System.Diagnostics;
using NetMQ;
using NProg.Distributed.Domain;
using NProg.Distributed.Messaging;
using NProg.Distributed.Messaging.Queries;
using NProg.Distributed.NetMQ.Messaging;
using NProg.Distributed.Service;

namespace NProg.Distributed.NetMQ
{
    public class NmqOrderClient : IHandler<Guid, Order>
    {
        private readonly NetMQContext context;
        private readonly NmqRequestQueue requestQueue;

        public NmqOrderClient(Uri serviceUri)
        {
            context = NetMQContext.Create();
            requestQueue = new NmqRequestQueue(context, serviceUri);
        }

        public void Add(Guid key, Order item)
        {
            requestQueue.Send(new Message
            {
                Body = new AddOrderRequest { Order = item }
            });

            requestQueue.Receive(x => Debug.Assert(x.BodyAs<StatusResponse>().Status));
        }

        public Order Get(Guid guid)
        {
            requestQueue.Send(new Message
            {
                Body = new GetOrderRequest { OrderId = guid }
            });

            Order order = null;
            requestQueue.Receive(x =>
            {
                order = x.BodyAs<GetOrderResponse>().Order;
            });

            return order;
        }

        public bool Remove(Guid guid)
        {
            requestQueue.Send(new Message
            {
                Body = new RemoveOrderRequest { OrderId = guid }
            });

            var status = false;
            requestQueue.Receive(x =>
            {
                status = x.BodyAs<StatusResponse>().Status;
            });

            return status;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing && requestQueue != null)
                requestQueue.Dispose();

            if (disposing && context != null)
                context.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}