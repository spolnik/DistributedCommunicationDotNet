using System;
using System.ServiceModel;
using NProg.Distributed.Domain;

namespace NProg.Distributed.WCF.Service
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        void Add(Guid key, Order item);

        [OperationContract]
        Order Get(Guid key);

        [OperationContract]
        bool Remove(Guid key);
    }
}