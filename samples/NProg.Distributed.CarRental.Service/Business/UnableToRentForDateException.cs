using System;

namespace NProg.Distributed.CarRental.Service.Business
{
    public class UnableToRentForDateException : ApplicationException
    {
        public UnableToRentForDateException(string message)
            : base(message)
        {
        }

        public UnableToRentForDateException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
