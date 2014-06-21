using System;
using NProg.Distributed.Common.Service.Messaging;
using NProg.Distributed.OrderService.Api;
using NProg.Distributed.OrderService.Domain;
using NProg.Distributed.OrderService.Requests;
using NProg.Distributed.OrderService.Responses;

namespace NProg.Distributed.OrderService
{
    /// <summary>
    /// Class OrderClient. This class cannot be inherited.
    /// </summary>
    public sealed class OrderClient : IOrderClient
    {
        /// <summary>
        /// The request sender
        /// </summary>
        private readonly IRequestSender requestSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderClient"/> class.
        /// </summary>
        /// <param name="requestSender">The request sender.</param>
        public OrderClient(IRequestSender requestSender)
        {
            this.requestSender = requestSender;
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="order">The order.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Add(Guid key, Order order)
        {
            return requestSender.Send(new AddOrderRequest { Order = order })
                .Receive<StatusResponse>().Status;
        }

        /// <summary>
        /// Gets the specified unique identifier.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns>Order.</returns>
        public Order Get(Guid guid)
        {
            return requestSender.Send(new GetOrderRequest { OrderId = guid })
                .Receive<GetOrderResponse>().Order;
        }

        /// <summary>
        /// Removes the specified unique identifier.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Remove(Guid guid)
        {
            return requestSender.Send(new RemoveOrderRequest { OrderId = guid })
                .Receive<StatusResponse>().Status;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposing && requestSender != null)
                requestSender.Dispose();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}