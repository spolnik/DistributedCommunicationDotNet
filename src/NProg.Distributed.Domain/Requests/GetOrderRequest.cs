using System;

namespace NProg.Distributed.OrderService.Requests
{
    [Serializable]
    public sealed class GetOrderRequest
    {
        public Guid OrderId { get; set; }
    }
}