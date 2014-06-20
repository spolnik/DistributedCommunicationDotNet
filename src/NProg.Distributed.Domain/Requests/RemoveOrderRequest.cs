using System;

namespace NProg.Distributed.OrderService.Requests
{
    [Serializable]
    public sealed class RemoveOrderRequest
    {
        public Guid OrderId { get; set; }
    }
}