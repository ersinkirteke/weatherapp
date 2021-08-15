using Appsfactory.Weather.Api.Services;
using Appsfactory.Weather.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Appsfactory.Weather.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : BaseController<WeatherController>
    {
        private readonly IWeatherForecastService _weatherForecastService;
        public WeatherController(IWeatherForecastService weatherForecastService)
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
        public async Task<ActionResult> GetWeatherForecastByCity([FromQuery] string city, [FromQuery] string zipCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrEmpty(city))
            {
                var result = await _weatherForecastService.GetWeatherForecastByCity(city);
                return Json(result);
            }
            else if (!string.IsNullOrEmpty(zipCode))
            {
                var result = await _weatherForecastService.GetWeatherForecastByZipCode(zipCode);
                return Json(result);
            }

            return NoContent();
        }

        [HttpGet]
        [Route("history")]
        public async Task<ActionResult> GetHistory()
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
