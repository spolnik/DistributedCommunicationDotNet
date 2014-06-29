using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Queries;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class GetReservationHandler 
        : MessageHandlerBase<GetReservationQuery, IReservationRepository>
    {
        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public GetReservationHandler(IReservationRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<GetReservationQuery,IReservationRepository>

        protected override IMessage Process(GetReservationQuery command)
        {
            var reservation = repository.Get(command.ReservationId);
            
            if (reservation == null)
            {
                throw new NotFoundException(string.Format("No reservation found for id '{0}'.", command.ReservationId));
            }

            return new GetReservationResponse {Reservation = reservation};
        }

        #endregion
    }
}