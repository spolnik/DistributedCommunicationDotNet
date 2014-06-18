using System;
using System.Diagnostics;
using NProg.Distributed.Domain;
using NProg.Distributed.Messaging;
using NProg.Distributed.Messaging.Queries;
using NProg.Distributed.Service;
using NProg.Distributed.ZeroMQ.Messaging;
using ZeroMQ;

namespace NProg.Distributed.ZeroMQ
{
    public class ZmqOrderClient : IHandler<Guid, Order>, IDisposable
    {
        private readonly ZmqContext context;
        private readonly ZmqMessageRequest messageRequest;

        public ZmqOrderClient(Uri serviceUri)
        {
            context = ZmqContext.Create();
            messageRequest = new ZmqMessageRequest(context, serviceUri);
        }

        public void Add(Guid key, Order item)
        {
            var response = messageRequest.Send(new Message
            {
                Body = new AddOrderRequest {Order = item}
            });

            Debug.Assert(response.BodyAs<StatusResponse>().Status);
        }

        public Order Get(Guid guid)
        {
            var response = messageRequest.Send(new Message
            {
                Body = new GetOrderRequest{ OrderId = guid }
            });

            return response.BodyAs<GetOrderResponse>().Order;
        }

        public bool Remove(Guid guid)
        {
            var response = messageRequest.Send(new Message
            {
                Body = new RemoveOrderRequest {OrderId = guid}
            });

            return response.BodyAs<StatusResponse>().Status;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing && messageRequest != null)
                messageRequest.Dispose();

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