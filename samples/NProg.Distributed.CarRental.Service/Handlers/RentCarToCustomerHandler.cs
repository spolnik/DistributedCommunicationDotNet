using System;
using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Business;
using NProg.Distributed.CarRental.Service.Commands;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class RentCarToCustomerHandler 
        : MessageHandlerBase<RentCarToCustomerCommand, IRentalRepository>
    {
        private readonly ICarRentalEngine carRentalEngine;

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public RentCarToCustomerHandler(IRentalRepository repository, ICarRentalEngine carRentalEngine) : base(repository)
        {
            this.carRentalEngine = carRentalEngine;
        }

        #region Overrides of MessageHandlerBase<RentCarToCustomerCommand,IRentalRepository>

        protected override IMessage Process(RentCarToCustomerCommand command)
        {
            var rental = carRentalEngine.RentCarToCustomer(command.LoginEmail, command.CarId,
                command.RentalDate ?? DateTime.Now, command.DateDueBack);

            return new GetRentalResponse {Rental = rental};
        }

        #endregion
    }
}