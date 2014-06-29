using System;
using System.Linq;
using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class GetDeadReservationsHandler 
        : MessageHandlerBase<GetDeadReservationsRequest, IReservationRepository>
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public GetDeadReservationsHandler(IReservationRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<GetDeadReservationsRequest,IReservationRepository>

        protected override IRequestResponse Process(GetDeadReservationsRequest request)
        {
            var reservations = repository.GetReservationsByPickupDate(DateTime.Now.AddDays(-1));

            return new GetReservationsResponse
                {
                    Reservations = (reservations != null
                        ? reservations.ToArray()
                        : null)
                };
        }

        #endregion
    }
}