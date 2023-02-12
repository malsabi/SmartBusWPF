namespace SmartBusWPF.Models
{
    public class LoginModel
    {
        public string BusDriverId { get; set; }
        public string Password { get; set; }
        public bool IsStaySignedIn { get; set; }
    }
}