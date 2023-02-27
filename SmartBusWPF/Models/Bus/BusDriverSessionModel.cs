namespace SmartBusWPF.Models.Bus
{
    public class BusDriverSessionModel
    {
        public BusDriverModel BusDriver { get; set; }
        public BusModel Bus { get; set; }
        public string AuthToken { get; set; }
        public bool IsActive { get; set; }
        public bool IsTripStarted { get; set; }
    }
}