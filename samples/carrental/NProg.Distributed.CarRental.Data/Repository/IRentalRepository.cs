using System.Collections.Generic;
using NProg.Distributed.CarRental.Data.DTO;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.Core.Data;

namespace NProg.Distributed.CarRental.Data.Repository
{
    public interface IRentalRepository : IDataRepository<int, Rental>
    {
        IEnumerable<Rental> GetRentalHistoryByCar(int carId);
        Rental GetCurrentRentalByCar(int carId);
        IEnumerable<Rental> GetCurrentlyRentedCars();
        IEnumerable<Rental> GetRentalHistoryByAccount(int accountId);
        IEnumerable<CustomerRentalInfo> GetCurrentCustomerRentalInfo();
    }
}
