using System.Text.Json.Serialization;

namespace SmartBusWPF.Models
{
    public class ProblemDetailsModel
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("status")]
        public int? Status { get; set; }
        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        public ProblemDetailsModel()
        {
            Type = string.Empty;
            Title = string.Empty;
            Status = null;
            Detail = string.Empty;
        }
    }
}