using System;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.OrderService.Api;
using NProg.Distributed.OrderService.Domain;
using NProg.Distributed.OrderService.Requests;
using NProg.Distributed.OrderService.Responses;

namespace NProg.Distributed.OrderService
{
    /// <summary>
    /// Class OrderClient. This class cannot be inherited.
    /// </summary>
    public sealed class OrderClient : MessageClientBase, IOrderApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public OrderClient(IMessageSender messageSender) : base(messageSender)
        {}

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="order">The order.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Add(Guid key, Order order)
        {
            return messageSender.Send(new AddOrderRequest { Order = order })
                .Receive<StatusResponse>().Status;
        }

        /// <summary>
        /// Gets the specified unique identifier.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns>Order.</returns>
        public Order Get(Guid guid)
        {
            return messageSender.Send(new GetOrderRequest { OrderId = guid })
                .Receive<GetOrderResponse>().Order;
        }

        /// <summary>
        /// Removes the specified unique identifier.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Remove(Guid guid)
        {
            return messageSender.Send(new RemoveOrderRequest { OrderId = guid })
                .Receive<StatusResponse>().Status;
        }
    }
}