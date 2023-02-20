using System.Text.Json.Serialization;

namespace SmartBusWPF.Models.HuskyLens
{
    public class FaceRecognitonModel
    {
        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("ID")]
        public int ID { get; set; }

        [JsonPropertyName("learned")]
        public bool Learned { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}