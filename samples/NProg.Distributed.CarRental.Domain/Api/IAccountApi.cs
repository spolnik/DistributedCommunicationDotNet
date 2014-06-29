namespace NProg.Distributed.CarRental.Domain.Api
{
    public interface IAccountApi
    {
        Account GetCustomerAccountInfo(string loginEmail);

        void UpdateCustomerAccountInfo(Account account); 
    }
}