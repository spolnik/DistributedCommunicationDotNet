using System;
using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Business;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class AcceptCarReturnHandler 
        : MessageHandlerBase<AcceptCarReturnRequest, IRentalRepository>
    {
        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public AcceptCarReturnHandler(IRentalRepository repository) : base(repository)
        {
        }

        #region Overrides of MessageHandlerBase<AcceptCarReturnRequest,IRentalRepository>

        protected override IRequestResponse Process(AcceptCarReturnRequest request)
        {
            var rental = repository.GetCurrentRentalByCar(request.CarId);

            if (rental == null)
            {
                throw new CarNotRentedException(string.Format("Car {0} is not currently rented.", request.CarId));
            }

            rental.DateReturned = DateTime.Now;

            var updatedRentalEntity = repository.Update(rental);

            return new StatusResponse {Status = updatedRentalEntity != null};
        }

        #endregion
    }
}