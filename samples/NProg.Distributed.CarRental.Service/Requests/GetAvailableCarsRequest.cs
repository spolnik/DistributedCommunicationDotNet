using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Requests
{
    [Serializable]
    public class GetAvailableCarsRequest : IRequestResponse
    {
         public DateTime PickupDate { get; set; }
         
        public DateTime ReturnDate { get; set; }
    }
}