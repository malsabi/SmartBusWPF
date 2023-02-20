using SmartBusWPF.DTOs.Bus;
using SmartBusWPF.DTOs.BusDriver;

namespace SmartBusWPF.DTOs.Auth.Login.Response
{
    public class LoginBusDriverResponseDto
    {
        public BusDriverDto BusDriverDto { get; set; }
        public BusDto BusDto { get; set; }
        public string AuthToken { get; set; }
    }
}