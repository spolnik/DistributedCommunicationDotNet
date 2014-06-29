using System.ServiceModel;
using NProg.Distributed.Core.Service.Messaging;

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