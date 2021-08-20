using Appsfactory.Weather.Domain.Entities;
using Appsfactory.Weather.Api.DTOs;
using Appsfactory.Weather.Infrastructure;
using Appsfactory.Weather.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System;
using System.Linq;
using Appsfactory.Weather.Domain.ValueObjects;
using Appsfactory.Weather.Domain.Constants;
using Appsfactory.Weather.Api.Extensions;
using Appsfactory.Weather.Api.Options;
using Microsoft.Extensions.Options;

namespace Appsfactory.Weather.Api.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IRepository<Forecast> _forecastRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _clientFactory;
        private readonly  WeatherOptions _options;

        public WeatherForecastService(IUnitOfWork unitOfWork, IHttpClientFactory clientFactory, IOptions<WeatherOptions>  options)
        {
            _clientFactory = clientFactory;
            _unitOfWork = unitOfWork;
            _options = options.Value;
            _forecastRepository = _unitOfWork.GetRepository<Forecast>();
        }

        public async Task<WeatherForecastResponse> GetWeatherForecastByCity(string city)
        {
            WeatherResponse weatherResponse = new WeatherResponse();
            var request = new HttpRequestMessage(HttpMethod.Get,
           $"{_options.ApiUrl}?appid={_options.ApiKey}&q={city}");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
                options.Converters.Add(new DateTimeOffsetJsonConverter());
                weatherResponse = await JsonSerializer.DeserializeAsync<WeatherResponse>(responseStream, options);
            }
            else
            {
                return new WeatherForecastResponse();
            }

            var result = weatherResponse.List.GroupBy(d => d.ForecastDate.ToString("yyyy/MM/dd")).Select(i => new
            ForecastDto
            {
                ForecastDate = i.Key,
                Items = i.ToList().Select(x => new
                ForecastItem
                {
                    Temperature = x.Main.Temp,
                    Humidity = x.Main.Humidity,
                    WindSpeed = x.Wind.Speed
                }),
                AvgTemperature = Math.Round(i.ToList().Average(x => x.Main.Temp), 2),
                AvgHumidity = Math.Round(i.ToList().Average(x => x.Main.Humidity), 2)
            });

            foreach (var item in weatherResponse.List)
            {
                _forecastRepository.Add(new Forecast
                {
                    CreatedDate = DateTime.Now,
                    Humidity = item.Main.Humidity,
                    Temperature = new Temperature(
                        item.Main.Temp,
                        new Scale
                        {
                            ScaleUnit = ScaleUnit.Kelvin,
                            DecimalPlaces = 2,
                            InUse = true
                        }),
                    ForecastDate = item.ForecastDate,
                    WindSpeed = item.Wind.Speed,
                    Address = new Address(city.FirstCharToUpper(), "")
                });
            }

            _unitOfWork.Commit();

            return new WeatherForecastResponse()
            {
                Forecasts = result.OrderByDescending(x => x.ForecastDate).ToList().AsReadOnly()
            };
        }

        public async Task<WeatherForecastResponse> GetWeatherForecastByZipCode(string zipCode)
        {
            WeatherResponse weatherResponse = new WeatherResponse();
            var request = new HttpRequestMessage(HttpMethod.Get,
           $"{_options.ApiUrl}?appid={_options.ApiKey}&zip={zipCode}");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
                options.Converters.Add(new DateTimeOffsetJsonConverter());
                weatherResponse = await JsonSerializer.DeserializeAsync<WeatherResponse>(responseStream, options);
            }
            else
            {
                return new WeatherForecastResponse();
            }

            var result = weatherResponse.List.GroupBy(d => d.ForecastDate.ToString("yyyy/MM/dd")).Select(i => new
            ForecastDto
            {
                ForecastDate = i.Key,
                Items = i.ToList().Select(x => new
                ForecastItem
                {
                    Temperature = x.Main.Temp,
                    Humidity = x.Main.Humidity,
                    WindSpeed = x.Wind.Speed
                }),
                AvgTemperature = Math.Round(i.ToList().Average(x => x.Main.Temp), 2),
                AvgHumidity = Math.Round(i.ToList().Average(x => x.Main.Humidity), 2)
            });

            foreach (var item in weatherResponse.List)
            {
                _forecastRepository.Add(new Forecast
                {
                    CreatedDate = DateTime.Now,
                    Humidity = item.Main.Humidity,
                    Temperature = new Temperature(
                        item.Main.Temp,
                        new Scale
                        {
                            ScaleUnit = ScaleUnit.Celcius,
                            DecimalPlaces = 2,
                            InUse = true
                        }),
                    ForecastDate = item.ForecastDate,
                    WindSpeed = item.Wind.Speed,
                    Address = new Address("", zipCode)
                });
            }

            _unitOfWork.Commit();

            return new WeatherForecastResponse()
            {
                Forecasts = result.OrderByDescending(x => x.ForecastDate).ToList().AsReadOnly()
            };
        }

        public IReadOnlyCollection<WeatherHistory> GetHistory()
        {
            List<WeatherHistory> weatherHistories = new List<WeatherHistory>();
            var forecasts = _forecastRepository.GetAll().OrderByDescending(x => x.CreatedDate);

            foreach (var item in forecasts)
            {
                weatherHistories.Add(new WeatherHistory
                {
                    Id = item.Id,
                    City = item.Address.City ?? "",
                    ZipCode = item.Address.ZipCode ?? "",
                    Temperature = item.Temperature.Value,
                    Humidity = item.Humidity,
                    WindSpeed = item.WindSpeed,
                    ForecastDate = item.ForecastDate.ToString("dd/MM/yyyy HH:mm:ss"),
                    CreatedDate = item.CreatedDate.ToString("dd/MM/yyyy HH:mm:ss")
                });
            }

            return weatherHistories.AsReadOnly();
        }
    }
}
