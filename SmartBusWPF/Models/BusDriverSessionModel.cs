namespace SmartBusWPF.Models
{
    public class BusDriverSessionModel
    {
        public BusDriverModel BusDriver { get; set; }
        public string AuthToken { get; set; }
        public bool IsActive { get; set; }
    }
}