using System.Collections.Generic;
using System.Linq;
using NProg.Distributed.CarRental.Data.DTO;
using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Domain.DTO;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class GetCurrentRentalsHandler 
        : MessageHandlerBase<GetCurrentRentalsRequest, IRentalRepository>
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public GetCurrentRentalsHandler(IRentalRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<GetCurrentRentalsRequest,IRentalRepository>

        protected override IRequestResponse Process(GetCurrentRentalsRequest request)
        {
            var rentalInfoSet = repository.GetCurrentCustomerRentalInfo();

            var customerRentalData = rentalInfoSet.Select(rentalInfo => new CustomerRentalData()
                {
                    RentalId = rentalInfo.Rental.RentalId,
                    Car = rentalInfo.Car.Color + " " + rentalInfo.Car.Year + " " + rentalInfo.Car.Description,
                    CustomerName = rentalInfo.Customer.FirstName + " " + rentalInfo.Customer.LastName,
                    DateRented = rentalInfo.Rental.DateRented,
                    ExpectedReturn = rentalInfo.Rental.DateDue
                }).ToArray();

            return new CustomerRentalDataResponse {CustomerRentalData = customerRentalData};
        }

        #endregion
    }
}