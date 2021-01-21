using System;
using System.Globalization;
using System.Text.Json.Serialization;

namespace consoleclient
{
    public class Municipality
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("municipality_name")]
        public string Municipality_name { get; set; }

        [JsonPropertyName("period_start")]
        public DateTime Period_start { get; set; }

        [JsonPropertyName("period_end")]
        public DateTime Period_end { get; set; }

        [JsonPropertyName("yearly")]
        public double? Yearly { get; set; }

        [JsonPropertyName("monthly")]
        public double? Monthly { get; set; }

        [JsonPropertyName("weekly")]
        public double? Weekly { get; set; }

        [JsonPropertyName("daily")]
        public double? Daily { get; set; }
    }
}