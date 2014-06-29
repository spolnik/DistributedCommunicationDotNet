using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class AddOrUpdateCarHandler 
        : MessageHandlerBase<AddOrUpdateCarRequest, ICarRepository>
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public AddOrUpdateCarHandler(ICarRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<AddOrUpdateCarRequest,ICarRepository>

        protected override IRequestResponse Process(AddOrUpdateCarRequest request)
        {
            var updatedEntity = request.Car.CarId == 0
                ? repository.Add(request.Car)
                : repository.Update(request.Car);

            return new GetCarResponse {Car = updatedEntity};
        }

        #endregion
    }
}