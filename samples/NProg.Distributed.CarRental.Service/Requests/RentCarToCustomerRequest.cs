using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Requests
{
    [Serializable]
    public class RentCarToCustomerRequest : IRequestResponse
    {
        public string LoginEmail { get; set; }

        public int CarId { get; set; }

        public DateTime? RentalDate { get; set; }

        public DateTime DateDueBack { get; set; }
    }
}