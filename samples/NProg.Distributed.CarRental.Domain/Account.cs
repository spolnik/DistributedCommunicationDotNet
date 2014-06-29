using System;
using NProg.Distributed.Core.Data;

namespace NProg.Distributed.CarRental.Domain
{
    [Serializable]
    public class Account : IIdentifiableEntity<int>
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

        #region Implementation of IIdentifiableEntity<int>

        public int EntityId
        {
            get { return AccountId; }
            set { AccountId = value; }
        }

        #endregion
    }
}
