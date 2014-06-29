using System;
using System.Collections.Generic;
using NProg.Distributed.CarRental.Data.DTO;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.Core.Data;

namespace NProg.Distributed.CarRental.Data.Repository
{
    public interface IReservationRepository : IDataRepository<int, Reservation>
    {
        IEnumerable<Reservation> GetReservationsByPickupDate(DateTime pickupDate);
        IEnumerable<CustomerReservationInfo> GetCurrentCustomerReservationInfo();
        IEnumerable<CustomerReservationInfo> GetCustomerOpenReservationInfo(int accountId);
    }
}
