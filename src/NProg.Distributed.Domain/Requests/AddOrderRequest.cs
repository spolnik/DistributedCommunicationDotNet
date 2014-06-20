using System;
using NProg.Distributed.OrderService.Domain;

namespace NProg.Distributed.OrderService.Requests
{
    [Serializable]
    public sealed class AddOrderRequest
    {
        public Order Order { get; set; }
    }
}