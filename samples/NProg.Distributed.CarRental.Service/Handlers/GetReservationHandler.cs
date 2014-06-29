using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class GetReservationHandler 
        : MessageHandlerBase<GetReservationRequest, IReservationRepository>
    {
        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public GetReservationHandler(IReservationRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<GetReservationRequest,IReservationRepository>

        protected override IRequestResponse Process(GetReservationRequest request)
        {
            var reservation = repository.Get(request.ReservationId);
            
            if (reservation == null)
            {
                throw new NotFoundException(string.Format("No reservation found for id '{0}'.", request.ReservationId));
            }

            return new GetReservationResponse {Reservation = reservation};
        }

        #endregion
    }
}