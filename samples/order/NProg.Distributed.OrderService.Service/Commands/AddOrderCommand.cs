using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.OrderService.Commands
{
    [Serializable]
    public sealed class AddOrderCommand : IMessage
    {
        public OrderService.Domain.Order Order { get; set; }
    }
}