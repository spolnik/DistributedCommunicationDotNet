using System;

namespace NProg.Distributed.CarRental.Domain.Api
{
    public interface IAccountApi : IDisposable
    {
        Account GetCustomerAccountInfo(string loginEmail);

        void UpdateCustomerAccountInfo(Account account); 
    }
}