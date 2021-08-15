using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Appsfactory.Weather.Api.DTOs
{
    public class WeatherForecastResponse
    {
        public IReadOnlyCollection<ForecastDto> Forecasts { get; set; }
    }
}
