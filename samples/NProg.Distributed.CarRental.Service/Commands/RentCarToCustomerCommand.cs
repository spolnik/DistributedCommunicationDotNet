using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Commands
{
    [Serializable]
    public class RentCarToCustomerCommand : IMessage
    {
        public string LoginEmail { get; set; }

        public int CarId { get; set; }

        public DateTime? RentalDate { get; set; }

        public DateTime DateDueBack { get; set; }
    }
}