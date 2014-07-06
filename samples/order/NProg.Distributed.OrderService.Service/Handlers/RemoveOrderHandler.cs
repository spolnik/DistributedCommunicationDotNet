using System;
using NProg.Distributed.Core.Data;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.OrderService.Commands;

namespace NProg.Distributed.OrderService.Handlers
{
    public sealed class RemoveOrderHandler : MessageHandlerBase<RemoveOrderCommand>
    {
        public RemoveOrderHandler(IDataRepository<Guid, OrderService.Domain.Order> orderRepository) 
            : base(orderRepository)
        {
        }

        protected override IMessage Process(RemoveOrderCommand command)
        {
            var status = repository.Remove(command.OrderId);
            return new StatusResponse { Status = status };
        }
    }
}