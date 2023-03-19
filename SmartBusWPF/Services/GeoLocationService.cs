using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using SmartBusWPF.Common.Consts;
using SmartBusWPF.Models.GeoLocation;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.Services
{
    public class GeoLocationService : IGeoLocationService
    {
        public async Task<GeoModel> GetCurrentLocation()
        {
            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(string.Format("https://ipgeolocation.abstractapi.com/v1/?api_key={0}", GeoLocationConsts.API_KEY));
                GeoModel content = await response.Content.ReadFromJsonAsync<GeoModel>();
                return content;
            }
            catch
            {
                return null;
            }
        }
    }
}