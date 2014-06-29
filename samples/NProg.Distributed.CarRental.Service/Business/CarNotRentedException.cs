using System;

namespace NProg.Distributed.CarRental.Service.Business
{
    public class CarNotRentedException : ApplicationException
    {
        public CarNotRentedException(string message)
            : base(message)
        {
        }

        public CarNotRentedException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
