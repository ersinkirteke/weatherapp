using System;
using System.Text.Json.Serialization;

namespace Appsfactory.Weather.Api.DTOs
{
    public class WeatherDto
    {
        public MainDto Main { get; set; }
        public WindDto Wind { get; set; }
        [JsonPropertyName("dt_txt")]
        public DateTimeOffset ForecastDate { get; set; }
    }
}
