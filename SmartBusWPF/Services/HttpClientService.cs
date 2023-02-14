using System;
using System.Net.Http;
using SmartBusWPF.Models;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using SmartBusWPF.Common.Consts;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.Services
{
    public class HttpClientService : IHttpClientService
    {
        public async Task<HttpResponseModel<TResponse>> GetAsync<TResponse>(string endpoint, string token = "", params string[] args)
        {
            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIConsts.Scheme, token);
                }
                client.BaseAddress = new Uri(APIConsts.LocalAPIEndPoint);
                HttpResponseMessage response = await client.GetAsync(string.Format(endpoint, args));
                if (response.IsSuccessStatusCode)
                {
                    return new HttpResponseModel<TResponse>
                    {
                        IsSuccess = true,
                        Response = await response.Content.ReadFromJsonAsync<TResponse>()
                    };
                }
                else
                {
                    return new HttpResponseModel<TResponse>
                    {
                        IsSuccess = false,
                        ProblemDetails = await response.Content.ReadFromJsonAsync<ProblemDetailsModel>()
                    };
                }
            }
            catch
            {
                return default;
            }
        }

        public async Task<HttpResponseModel<TResponse>> PostAsync<TRequest, TResponse>(TRequest data, string endpoint, string token = "")
        {
            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIConsts.Scheme, token);
                }
                client.BaseAddress = new Uri(APIConsts.LocalAPIEndPoint);
                HttpResponseMessage response = await client.PostAsJsonAsync(endpoint, data);
                if (response.IsSuccessStatusCode)
                {
                    return new HttpResponseModel<TResponse>
                    {
                        IsSuccess = true,
                        Response = await response.Content.ReadFromJsonAsync<TResponse>()
                    };
                }
                else
                {
                    return new HttpResponseModel<TResponse>
                    {
                        IsSuccess = false,
                        ProblemDetails = await response.Content.ReadFromJsonAsync<ProblemDetailsModel>()
                    };
                }
            }
            catch
            {
                return default;
            }
        }

        public async Task<HttpResponseModel<TResponse>> PutAsync<TRequest, TResponse>(TRequest data, string endpoint, string token = "", params string[] args)
        {
            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIConsts.Scheme, token);
                }
                client.BaseAddress = new Uri(APIConsts.LocalAPIEndPoint);
                HttpResponseMessage response = await client.PutAsJsonAsync(string.Format(endpoint, args), data);
                if (response.IsSuccessStatusCode)
                {
                    return new HttpResponseModel<TResponse>
                    {
                        IsSuccess = true,
                        Response = await response.Content.ReadFromJsonAsync<TResponse>()
                    };
                }
                else
                {
                    return new HttpResponseModel<TResponse>
                    {
                        IsSuccess = false,
                        ProblemDetails = await response.Content.ReadFromJsonAsync<ProblemDetailsModel>()
                    };
                }
            }
            catch
            {
                return default;
            }
        }

        public async Task<HttpResponseModel<TResponse>> DeleteAsync<TResponse>(string endpoint, string token = "", params string[] args)
        {
            try
            {
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIConsts.Scheme, token);
                }
                client.BaseAddress = new Uri(APIConsts.LocalAPIEndPoint);
                HttpResponseMessage response = await client.DeleteAsync(string.Format(endpoint, args));
                if (response.IsSuccessStatusCode)
                {
                    return new HttpResponseModel<TResponse>
                    {
                        IsSuccess = true,
                        Response = await response.Content.ReadFromJsonAsync<TResponse>()
                    };
                }
                else
                {
                    return new HttpResponseModel<TResponse>
                    {
                        IsSuccess = false,
                        ProblemDetails = await response.Content.ReadFromJsonAsync<ProblemDetailsModel>()
                    };
                }
            }
            catch
            {
                return default;
            }
        }
    }
}