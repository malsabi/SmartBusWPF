﻿using SmartBusWPF.Common.Enums;

namespace SmartBusWPF.DTOs.Trip
{
    public class StartTripDto
    {
        public int BusID { get; set; }
        public DestinationType DestinationType { get; set; }
    }
}