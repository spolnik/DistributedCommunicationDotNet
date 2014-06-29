using System;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.OrderService.Domain;

namespace NProg.Distributed.OrderService.Responses
{
    [Serializable]
    public sealed class GetOrderResponse : IRequestResponse
    {
        public Order Order { get; set; } 
    }
}