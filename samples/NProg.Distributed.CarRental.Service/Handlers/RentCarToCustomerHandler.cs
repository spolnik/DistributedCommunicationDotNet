using System;
using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Business;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class RentCarToCustomerHandler 
        : MessageHandlerBase<RentCarToCustomerRequest, IRentalRepository>
    {
        private readonly ICarRentalEngine carRentalEngine;

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public RentCarToCustomerHandler(IRentalRepository repository, ICarRentalEngine carRentalEngine) : base(repository)
        {
            this.carRentalEngine = carRentalEngine;
        }

        #region Overrides of MessageHandlerBase<RentCarToCustomerRequest,IRentalRepository>

        protected override IRequestResponse Process(RentCarToCustomerRequest request)
        {
            var rental = carRentalEngine.RentCarToCustomer(request.LoginEmail, request.CarId,
                request.RentalDate ?? DateTime.Now, request.DateDueBack);

            return new GetRentalResponse {Rental = rental};
        }

        #endregion
    }
}