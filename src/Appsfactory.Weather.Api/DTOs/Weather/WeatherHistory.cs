using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appsfactory.Weather.Api.DTOs
{
    public class WeatherHistory
    {
        public long Id { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal WindSpeed { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string ForecastDate { get; set; }
        public string CreatedDate { get; set; }
    }
}
