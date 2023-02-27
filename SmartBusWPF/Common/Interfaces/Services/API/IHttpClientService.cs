using SmartBusWPF.Models.API;
using System.Threading.Tasks;

namespace SmartBusWPF.Common.Interfaces.Services.API
{
    public interface IHttpClientService
    {
        Task<HttpResponseModel<TResponse>> GetAsync<TResponse>(string endpoint, string token = "", params string[] args);
        Task<HttpResponseModel<TResponse>> PostAsync<TRequest, TResponse>(TRequest data, string endpoint, string token = "");
        Task<HttpResponseModel<TResponse>> PutAsync<TRequest, TResponse>(TRequest data, string endpoint, string token = "", params string[] args);
        Task<HttpResponseModel<TResponse>> DeleteAsync<TResponse>(string endpoint, string token = "", params string[] args);
    }
}