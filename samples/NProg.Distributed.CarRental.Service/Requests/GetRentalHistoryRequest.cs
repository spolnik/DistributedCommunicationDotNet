using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Requests
{
    [Serializable]
    public class GetRentalHistoryRequest : IRequestResponse
    {
        public string LoginEmail { get; set; }
    }
}