using Appsfactory.Weather.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Appsfactory.Weather.Api.Services
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecastResponse> GetWeatherForecastByCity(string city);
        Task<WeatherForecastResponse> GetWeatherForecastByZipCode(string zipCode);
        IReadOnlyCollection<WeatherHistory> GetHistory();
    }
}
