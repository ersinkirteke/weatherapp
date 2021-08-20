using Appsfactory.Weather.Api.Services;
using Appsfactory.Weather.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Appsfactory.Weather.Api.Options;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace Appsfactory.Weather.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WeatherController : BaseController<WeatherController>
    {
        private readonly IWeatherForecastService _weatherForecastService;
        public WeatherController(IWeatherForecastService weatherForecastService, IOptions<WeatherOptions> options) : base(options)
        {
            _weatherForecastService = weatherForecastService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forecast"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("forecast")]
        public async Task<ActionResult> GetWeatherForecast([FromQuery]WeatherForecastRequest weatherForecastRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (weatherForecastRequest != null && !string.IsNullOrEmpty(weatherForecastRequest.City))
            {
                var result = await _weatherForecastService.GetWeatherForecastByCity(weatherForecastRequest.City);

                if (result != null && result.Forecasts.Count > 0)
                    return Json(result);
                else
                    return NoContent();
            }
            else if (weatherForecastRequest != null && !string.IsNullOrEmpty(weatherForecastRequest.ZipCode))
            {
                var result = await _weatherForecastService.GetWeatherForecastByZipCode(weatherForecastRequest.ZipCode);

                if (result != null && result.Forecasts.Count > 0)
                    return Json(result);
                else
                    return NoContent();
            }

            return NoContent();
        }

        [HttpGet]
        [Route("history")]
        public ActionResult GetHistory()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var result = _weatherForecastService.GetHistory();

            if (result != null && result.Count > 0)
                return Json(result);

            return NoContent();
        }
    }
}
