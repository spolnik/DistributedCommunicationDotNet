using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.Xml;

namespace NProg.Distributed.WCF.Service
{
    internal sealed class NetDataContractOperationBehavior : DataContractSerializerOperationBehavior
    {
        internal NetDataContractOperationBehavior(OperationDescription operation) : base(operation)
        {
        }

        public override XmlObjectSerializer CreateSerializer(
            Type type, string name, string ns, IList<Type> knownTypes)
        {
            return new NetDataContractSerializer(name, ns);
        }
 
        public override XmlObjectSerializer CreateSerializer(
            Type type, XmlDictionaryString name, XmlDictionaryString ns,
            IList<Type> knownTypes)
        {
            return new NetDataContractSerializer(name, ns);
        }
    }
}