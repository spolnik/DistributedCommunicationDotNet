using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Queries
{
    [Serializable]
    public class GetAvailableCarsQuery : IMessage
    {
         public DateTime PickupDate { get; set; }
         
        public DateTime ReturnDate { get; set; }
    }
}