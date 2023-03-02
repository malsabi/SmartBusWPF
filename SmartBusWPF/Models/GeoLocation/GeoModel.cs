using System.Text.Json.Serialization;

namespace SmartBusWPF.Models.GeoLocation
{
    public class GeoModel
    {
        [JsonPropertyName("longitude")]
        public float Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public float Latitude { get; set; }
    }
}