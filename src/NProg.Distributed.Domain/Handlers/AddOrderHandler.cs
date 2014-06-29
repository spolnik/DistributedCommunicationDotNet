using System;
using NProg.Distributed.Core.Data;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.OrderService.Domain;
using NProg.Distributed.OrderService.Requests;
using NProg.Distributed.OrderService.Responses;

namespace NProg.Distributed.OrderService.Handlers
{
    public sealed class AddOrderHandler : MessageHandlerBase<AddOrderCommand>
    {
        public AddOrderHandler(IDataRepository<Guid, Order> orderRepository) 
            : base(orderRepository)
        {
        }

        protected override IMessage Process(AddOrderCommand command)
        {
            var addedOrder = repository.Add(command.Order);
            return new StatusResponse {Status = addedOrder != null};
        }
    }
}