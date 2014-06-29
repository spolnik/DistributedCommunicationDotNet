using System.Linq;
using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Domain.DTO;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class GetCurrentReservationsHandler 
        : MessageHandlerBase<GetCurrentReservationsRequest, IReservationRepository>
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public GetCurrentReservationsHandler(IReservationRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<GetCurrentReservationsRequest,IReservationRepository>

        protected override IRequestResponse Process(GetCurrentReservationsRequest request)
        {
            var reservationInfoSet = repository.GetCurrentCustomerReservationInfo();

            var customerReservationData =
                reservationInfoSet.Select(reservationInfo => new CustomerReservationData
                    {
                        ReservationId = reservationInfo.Reservation.ReservationId,
                        Car = reservationInfo.Car.Color + " " + reservationInfo.Car.Year + " " + reservationInfo.Car.Description,
                        CustomerName = reservationInfo.Customer.FirstName + " " + reservationInfo.Customer.LastName,
                        RentalDate = reservationInfo.Reservation.RentalDate,
                        ReturnDate = reservationInfo.Reservation.ReturnDate
                    }).ToArray();

            return new CustomerReservationDataResponse {CustomerReservationData = customerReservationData};
        }

        #endregion
    }
}