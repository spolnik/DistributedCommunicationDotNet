using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class GetRentalHandler 
        : MessageHandlerBase<GetRentalRequest, IRentalRepository>
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public GetRentalHandler(IRentalRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<GetRentalHistoryRequest,IRentalRepository>

        protected override IRequestResponse Process(GetRentalRequest request)
        {
            var rental = repository.Get(request.RentalId);

            if (rental == null)
            {
                throw new NotFoundException(string.Format("No rental record found for id '{0}'.", request.RentalId));
            }

            return new GetRentalResponse {Rental = rental};
        }

        #endregion
    }
}