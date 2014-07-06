using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.OrderService.Responses
{
    [Serializable]
    public sealed class GetOrderResponse : IMessage
    {
        public OrderService.Domain.Order Order { get; set; } 
    }
}