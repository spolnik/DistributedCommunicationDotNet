using System.ServiceModel;
using Message = NProg.Distributed.Service.Messaging.Message;

namespace NProg.Distributed.WCF.Service
{
    [ServiceContract]
    public interface IMessageService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        Message Send(Message message);
    }

}