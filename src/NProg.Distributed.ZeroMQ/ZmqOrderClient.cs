using System;
using System.Diagnostics;
using NProg.Distributed.Domain;
using NProg.Distributed.Messaging;
using NProg.Distributed.Messaging.Queries;
using NProg.Distributed.Service;
using NProg.Distributed.ZeroMQ.Messaging;
using ZMQ;

namespace NProg.Distributed.ZeroMQ
{
    public class ZmqOrderClient : IHandler<Order>, IDisposable
    {
        private readonly Context context;
        private readonly ZmqRequestQueue requestQueue;

        public ZmqOrderClient(Uri serviceUri)
        {
            context = new Context();
            requestQueue = new ZmqRequestQueue(context, serviceUri);
        }

        public void Add(Order item)
        {
            requestQueue.Send(new Message
            {
                Body = new AddOrderRequest {Order = item}
            });

            requestQueue.Receive(x => Debug.Assert(x.BodyAs<StatusResponse>().Status));
        }

        public Order Get(Guid guid)
        {
            requestQueue.Send(new Message
            {
                Body = new GetOrderRequest{ OrderId = guid }
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