using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Queries;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class GetCarHandler 
        : MessageHandlerBase<GetCarQuery, ICarRepository>
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public GetCarHandler(ICarRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<GetCarQuery,ICarRepository>

        protected override IMessage Process(GetCarQuery command)
        {
            var carEntity = repository.Get(command.CarId);

            if (carEntity == null)
            {
                throw new NotFoundException(string.Format("Car with ID of {0} is not in database", command.CarId));
            }

            return new GetCarResponse {Car = carEntity};
        }

        #endregion
    }
}