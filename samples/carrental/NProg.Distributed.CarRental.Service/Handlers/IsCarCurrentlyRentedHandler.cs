using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Business;
using NProg.Distributed.CarRental.Service.Queries;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class IsCarCurrentlyRentedHandler : MessageHandlerBase<IsCarCurrentlyRentedQuery, IRentalRepository>
    {
        private readonly ICarRentalEngine carRentalEngine;

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public IsCarCurrentlyRentedHandler(IRentalRepository repository, ICarRentalEngine carRentalEngine)
            : base(repository)
        {
            this.carRentalEngine = carRentalEngine;
        }

        #region Overrides of MessageHandlerBase<IsCarCurrentlyRentedQuery,IRentalRepository>

        protected override IMessage Process(IsCarCurrentlyRentedQuery command)
        {
            return new StatusResponse {Status = carRentalEngine.IsCarCurrentlyRented(command.CarId)};
        }

        #endregion
    }
}