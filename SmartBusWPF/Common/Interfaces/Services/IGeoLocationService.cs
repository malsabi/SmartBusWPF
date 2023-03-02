using SmartBusWPF.Models.GeoLocation;
using System.Threading.Tasks;

namespace SmartBusWPF.Common.Interfaces.Services
{
    public interface IGeoLocationService
    {
        public Task<GeoModel> GetCurrentLocation();
    }
}