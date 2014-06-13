using System;
using System.ServiceModel;
using NProg.Distributed.Domain;

namespace NProg.Distributed.WCF.Service
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        void Add(Order item);

        [OperationContract]
        Order Get(Guid guid);

        [OperationContract]
        bool Remove(Guid guid);
    }
}