using System;
using NProg.Distributed.Domain.Api;
using NProg.Distributed.Domain.Requests;
using NProg.Distributed.Domain.Responses;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Domain
{
    public class OrderClient : IOrderApi
    {
        private readonly IRequestSender requestSender;

        public OrderClient(IRequestSender requestSender)
        {
            this.requestSender = requestSender;
        }

        public bool Add(Guid key, Order order)
        {
            return requestSender.Send(new AddOrderRequest { Order = order })
                .Receive<StatusResponse>().Status;
        }

        public Order Get(Guid guid)
        {
            return requestSender.Send(new GetOrderRequest { OrderId = guid })
                .Receive<GetOrderResponse>().Order;
        }

        public bool Remove(Guid guid)
        {
            return requestSender.Send(new RemoveOrderRequest { OrderId = guid })
                .Receive<StatusResponse>().Status;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing && requestSender != null)
                requestSender.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}