using System;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.OrderService.Commands;
using NProg.Distributed.OrderService.Domain.Api;
using NProg.Distributed.OrderService.Queries;
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
        public bool Add(Guid key, Domain.Order order)
        {
            return messageSender.Send(new AddOrderCommand { Order = order })
                .Receive<StatusResponse>().Status;
        }

        /// <summary>
        /// Gets the specified unique identifier.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns>Order.</returns>
        public OrderService.Domain.Order Get(Guid guid)
        {
            return messageSender.Send(new GetOrderQuery { OrderId = guid })
                .Receive<GetOrderResponse>().Order;
        }

        /// <summary>
        /// Removes the specified unique identifier.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Remove(Guid guid)
        {
            return messageSender.Send(new RemoveOrderCommand { OrderId = guid })
                .Receive<StatusResponse>().Status;
        }
    }
}