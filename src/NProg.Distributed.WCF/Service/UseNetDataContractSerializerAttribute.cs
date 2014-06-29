using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace NProg.Distributed.Transport.WCF.Service
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class UseNetDataContractSerializerAttribute : Attribute, IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription description,
            BindingParameterCollection parameters)
        {
        }
 
        public void ApplyClientBehavior(OperationDescription description,
            System.ServiceModel.Dispatcher.ClientOperation proxy)
        {
            ReplaceDataContractSerializerOperationBehavior(description);
        }
 
        public void ApplyDispatchBehavior(OperationDescription description,
            System.ServiceModel.Dispatcher.DispatchOperation dispatch)
        {
            ReplaceDataContractSerializerOperationBehavior(description);
        }
 
        public void Validate(OperationDescription description)
        {
        }
 
        private static void ReplaceDataContractSerializerOperationBehavior(
            OperationDescription description)
        {
            var dcsOperationBehavior =
                description.Behaviors.Find<DataContractSerializerOperationBehavior>();
 
            if (dcsOperationBehavior != null)
            {
                description.Behaviors.Remove(dcsOperationBehavior);
                description.Behaviors.Add(new NetDataContractOperationBehavior(description));
            }
        }
    }
}