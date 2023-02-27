using System.Threading.Tasks;
using SmartBusWPF.Models.API;
using SmartBusWPF.Common.Consts;
using SmartBusWPF.DTOs.Auth.Login.Request;
using SmartBusWPF.DTOs.Auth.Login.Response;
using SmartBusWPF.Common.Interfaces.Services.API;

namespace SmartBusWPF.Services.API
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientService httpClientService;

        public AuthService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public async Task<HttpResponseModel<LoginBusDriverResponseDto>> Login(string driverID, string password)
        {
            LoginBusDriverRequestDto loginDriverDto = new()
            {
                DriverID = driverID,
                Password = password
            };

            HttpResponseModel<LoginBusDriverResponseDto> result = await httpClientService.PostAsync<LoginBusDriverRequestDto, LoginBusDriverResponseDto>(loginDriverDto, APIConsts.Auth.LoginBusDriver);
            return result;
        }
    }
}