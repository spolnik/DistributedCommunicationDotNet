using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class CancelReservationHandler 
        : MessageHandlerBase<CancelReservationRequest, IReservationRepository>
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public CancelReservationHandler(IReservationRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<CancelReservationRequest,IReservationRepository>

        protected override IRequestResponse Process(CancelReservationRequest request)
        {
            Reservation reservation = repository.Get(request.ReservationId);
            
            if (reservation == null)
            {
                throw new NotFoundException(string.Format("No reservation found found for ID '{0}'.", request.ReservationId));
            }

            return new StatusResponse {Status = repository.Remove(request.ReservationId)};
        }

        #endregion
    }
}