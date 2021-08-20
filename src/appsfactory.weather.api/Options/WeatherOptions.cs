using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appsfactory.Weather.Api.Options
{
    public class WeatherOptions
    {
        public const string WeatherAPI = "WeatherApi";
        public string ApiKey { get; set; }
        public string ApiUrl { get; set; }
    }
}
