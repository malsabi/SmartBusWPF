using System.Threading.Tasks;
using SmartBusWPF.Models.API;
using SmartBusWPF.DTOs.Auth.Login.Response;

namespace SmartBusWPF.Common.Interfaces.Services.API
{
    public interface IAuthService
    {
        public Task<HttpResponseModel<LoginBusDriverResponseDto>> Login(string busDriverID, string password);
    }
}