using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appsfactory.Weather.Api.DTOs
{
    public class ForecastDto
    {
        public decimal AvgTemperature { get; set; }
        public decimal AvgHumidity { get; set; }
        public IEnumerable<ForecastItem> Items { get; set; }
        public string ForecastDate { get; set; }
    }
}
