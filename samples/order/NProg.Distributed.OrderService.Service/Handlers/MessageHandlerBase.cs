using System;
using NProg.Distributed.Core.Data;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.OrderService.Handlers
{
    public abstract class MessageHandlerBase<TRequest> 
        : MessageHandlerBase<TRequest, IDataRepository<Guid, OrderService.Domain.Order>> where TRequest : IMessage
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        protected MessageHandlerBase(IDataRepository<Guid, OrderService.Domain.Order> repository) 
            : base(repository)
        {
        }
    }
}