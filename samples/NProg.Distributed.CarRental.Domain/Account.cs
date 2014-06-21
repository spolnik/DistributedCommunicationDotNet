using System;

namespace NProg.Distributed.CarRental.Domain
{
    [Serializable]
    public class Account
    {
        public int AccountId { get; set; }

        public string LoginEmail { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string CreditCard { get; set; }

        public string ExpDate { get; set; }
    }
}
