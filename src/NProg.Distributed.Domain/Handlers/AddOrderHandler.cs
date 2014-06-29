using System;
using NProg.Distributed.Core.Data;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.OrderService.Domain;
using NProg.Distributed.OrderService.Requests;
using NProg.Distributed.OrderService.Responses;

namespace NProg.Distributed.OrderService.Handlers
{
    public sealed class AddOrderHandler : MessageHandlerBase<AddOrderRequest>
    {
        public AddOrderHandler(IDataRepository<Guid, Order> orderRepository) 
            : base(orderRepository)
        {
        }

        protected override IRequestResponse Process(AddOrderRequest request)
        {
            var addedOrder = repository.Add(request.Order);
            return new StatusResponse {Status = addedOrder != null};
        }
    }
}