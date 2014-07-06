using System;
using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Business;
using NProg.Distributed.CarRental.Service.Commands;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class AcceptCarReturnHandler 
        : MessageHandlerBase<AcceptCarReturnCommand, IRentalRepository>
    {
        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public AcceptCarReturnHandler(IRentalRepository repository) : base(repository)
        {
        }

        #region Overrides of MessageHandlerBase<AcceptCarReturnCommand,IRentalRepository>

        protected override IMessage Process(AcceptCarReturnCommand command)
        {
            var rental = repository.GetCurrentRentalByCar(command.CarId);

            if (rental == null)
            {
                throw new CarNotRentedException(string.Format("Car {0} is not currently rented.", command.CarId));
            }

            rental.DateReturned = DateTime.Now;

            var updatedRentalEntity = repository.Update(rental);

            return new StatusResponse {Status = updatedRentalEntity != null};
        }

        #endregion
    }
}