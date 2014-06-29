using System.Linq;
using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Domain.DTO;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class GetCustomerReservationsHandler 
        : MessageHandlerBase<GetCustomerReservationsRequest, IReservationRepository>
    {
        private readonly IAccountRepository accountRepository;

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public GetCustomerReservationsHandler(IReservationRepository repository, IAccountRepository accountRepository)
            : base(repository)
        {
            this.accountRepository = accountRepository;
        }

        #region Overrides of MessageHandlerBase<GetCustomerReservationsRequest,IReservationRepository>

        protected override IRequestResponse Process(GetCustomerReservationsRequest request)
        {
            var account = accountRepository.GetByLogin(request.LoginEmail);

            if (account == null)
            {
                throw new NotFoundException(string.Format("No account found for login '{0}'.", request.LoginEmail));
            }

            var reservationInfoSet = repository.GetCustomerOpenReservationInfo(account.AccountId);

            var customerReservationData = reservationInfoSet.Select(reservationInfo => new CustomerReservationData
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