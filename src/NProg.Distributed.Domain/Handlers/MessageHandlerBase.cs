using System;
using NProg.Distributed.Core.Data;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.OrderService.Domain;

namespace NProg.Distributed.OrderService.Handlers
{
    public abstract class MessageHandlerBase<TRequest> 
        : MessageHandlerBase<TRequest, IDataRepository<Guid, Order>> where TRequest : IRequestResponse
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        protected MessageHandlerBase(IDataRepository<Guid, Order> repository) 
            : base(repository)
        {
        }
    }
}