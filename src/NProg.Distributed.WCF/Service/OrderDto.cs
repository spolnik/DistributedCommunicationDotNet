using System;
using System.Runtime.Serialization;

namespace NProg.Distributed.WCF.Service
{
    [DataContract]
    public class OrderDto
    {
        [DataMember]
        public Guid OrderId { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public DateTime OrderDate { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public string UserName { get; set; } 
    }
}