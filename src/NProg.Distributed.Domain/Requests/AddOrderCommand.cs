using System;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.OrderService.Domain;

namespace NProg.Distributed.OrderService.Requests
{
    [Serializable]
    public sealed class AddOrderCommand : IMessage
    {
        public Order Order { get; set; }
    }
}