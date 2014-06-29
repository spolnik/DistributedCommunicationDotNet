using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Requests
{
    [Serializable]
    public class GetRentalRequest : IRequestResponse
    {
        public int RentalId { get; set; }
    }
}