﻿using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Requests
{
    [Serializable]
    public class GetCarRequest : IRequestResponse
    {
        public int CarId { get; set; }
    }
}