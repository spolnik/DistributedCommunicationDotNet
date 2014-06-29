using System;
using NProg.Distributed.Core.Data;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.OrderService.Domain;
using NProg.Distributed.OrderService.Requests;
using NProg.Distributed.OrderService.Responses;

namespace NProg.Distributed.OrderService.Handlers
{
    public sealed class GetOrderHandler : MessageHandlerBase<GetOrderQuery>
    {
        public GetOrderHandler(IDataRepository<Guid, Order> orderRepository) 
            : base(orderRepository)
        {
        }

        protected override IMessage Process(GetOrderQuery command)
        {
            var order = repository.Get(command.OrderId);

            if (order == null)
            {
                throw new NotFoundException(string.Format("Order not found for id: {0}", command.OrderId));
            }

            return new GetOrderResponse {Order = order};
        }
    }
}