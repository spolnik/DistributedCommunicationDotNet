using System;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.CarRental.Domain.Api;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service
{
    public class AccountClient : MessageClientBase, IAccountApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public AccountClient(IRequestSender requestSender) : base(requestSender)
        {}

        #region IAccountApi Members

        public Account GetCustomerAccountInfo(string loginEmail)
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomerAccountInfo(Account account)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}