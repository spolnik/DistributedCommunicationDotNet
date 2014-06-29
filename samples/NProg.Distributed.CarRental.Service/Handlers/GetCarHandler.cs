using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class GetCarHandler 
        : MessageHandlerBase<GetCarRequest, ICarRepository>
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public GetCarHandler(ICarRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<GetCarRequest,ICarRepository>

        protected override IRequestResponse Process(GetCarRequest request)
        {
            var carEntity = repository.Get(request.CarId);

            return carEntity == null
                ? new GetCarResponse {Car = new Car()}
                : new GetCarResponse {Car = carEntity};
        }

        #endregion
    }
}