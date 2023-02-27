using SmartBusWPF.Common.Enums;

namespace SmartBusWPF.Models.Bus
{
    public class BusModel
    {
        public int ID { get; set; }
        public int LicenseNumber { get; set; }
        public int Capacity { get; set; }
        public string CurrentLocation { get; set; }
        public DestinationType DestinationType { get; set; }
        public bool IsInService { get; set; }
    }
}