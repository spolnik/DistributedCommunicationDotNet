using System;
using NProg.Distributed.OrderService.Domain;

namespace NProg.Distributed.OrderService.Responses
{
    [Serializable]
    public sealed class GetOrderResponse
    {
        public Order Order { get; set; } 
    }
}