using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Appsfactory.Weather.Api.DTOs
{
    public class WeatherForecastRequest
    {
        [FromQuery(Name = "city")]
        public string City { get; set; }
        [FromQuery(Name = "zipCode")]
        public string ZipCode { get; set; }
    }
}
