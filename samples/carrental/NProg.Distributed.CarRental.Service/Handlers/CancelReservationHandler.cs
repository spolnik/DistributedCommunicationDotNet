using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.CarRental.Service.Commands;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class CancelReservationHandler 
        : MessageHandlerBase<CancelReservationCommand, IReservationRepository>
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public CancelReservationHandler(IReservationRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<CancelReservationCommand,IReservationRepository>

        protected override IMessage Process(CancelReservationCommand command)
        {
            Reservation reservation = repository.Get(command.ReservationId);
            
            if (reservation == null)
            {
                throw new NotFoundException(string.Format("No reservation found found for ID '{0}'.", command.ReservationId));
            }

            return new StatusResponse {Status = repository.Remove(command.ReservationId)};
        }

        #endregion
    }
}