using System;
using System.ServiceModel;

namespace NProg.Distributed.WCF.Service
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        void Add(OrderDto item);

        [OperationContract]
        OrderDto Get(Guid guid);

        [OperationContract]
        bool Remove(Guid guid);
    }
}