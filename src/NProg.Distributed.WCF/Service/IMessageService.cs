using System.ServiceModel;
using NProg.Distributed.Common.Service.Messaging;

namespace NProg.Distributed.Transport.WCF.Service
{
    [ServiceContract]
    internal interface IMessageService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        Message Send(Message message);
    }

}