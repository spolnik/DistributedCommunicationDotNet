﻿using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Requests
{
    [Serializable]
    public class GetCustomerReservationsRequest : IRequestResponse
    {
        public string LoginEmail { get; set; }
    }
}