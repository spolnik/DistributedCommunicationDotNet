using System;
using NProg.Distributed.Core.Data;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.OrderService.Domain;
using NProg.Distributed.OrderService.Requests;
using NProg.Distributed.OrderService.Responses;

namespace NProg.Distributed.OrderService.Handlers
{
    public sealed class RemoveOrderHandler : MessageHandlerBase<RemoveOrderRequest>
    {
        public RemoveOrderHandler(IDataRepository<Guid, Order> orderRepository) 
            : base(orderRepository)
        {
        }

        protected override IRequestResponse Process(RemoveOrderRequest request)
        {
            var status = repository.Remove(request.OrderId);
            return new StatusResponse { Status = status };
        }
    }
}