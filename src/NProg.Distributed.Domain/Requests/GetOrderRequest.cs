using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.OrderService.Requests
{
    [Serializable]
    public sealed class GetOrderRequest : IRequestResponse
    {
        public Guid OrderId { get; set; }
    }
}