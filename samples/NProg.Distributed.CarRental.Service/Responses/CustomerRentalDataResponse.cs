using System;
using NProg.Distributed.CarRental.Domain.DTO;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Responses
{
    [Serializable]
    public class CustomerRentalDataResponse : IRequestResponse
    {
        public CustomerRentalData[] CustomerRentalData { get; set; }
    }
}