using System;
using NProg.Distributed.Core.Data;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.OrderService.Domain;
using NProg.Distributed.OrderService.Requests;
using NProg.Distributed.OrderService.Responses;

namespace NProg.Distributed.OrderService.Handlers
{
    public sealed class GetOrderHandler : MessageHandlerBase<GetOrderRequest>
    {
        public GetOrderHandler(IDataRepository<Guid, Order> orderRepository) 
            : base(orderRepository)
        {
        }

        protected override IRequestResponse Process(GetOrderRequest request)
        {
            var order = repository.Get(request.OrderId);
            return new GetOrderResponse {Order = order};
        }
    }
}