using appsfactory.weather.api.test.Constants;
using Appsfactory.Weather.Api.Controllers;
using Appsfactory.Weather.Api.DTOs;
using Appsfactory.Weather.Api.Options;
using Appsfactory.Weather.Api.Services;
using Appsfactory.Weather.Domain.Constants;
using Appsfactory.Weather.Domain.Entities;
using Appsfactory.Weather.Domain.ValueObjects;
using Appsfactory.Weather.Infrastructure;
using Appsfactory.Weather.Infrastructure.Context;
using Appsfactory.Weather.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Appsfactory.Weather.Api.Test
{
    public class WeatherControllerTest
    {
        public WeatherControllerTest()
        {

        }

        [Fact]
        public async Task Forecast_Get_By_City_Succeded()
        {
            //arrange
            var cityJson = "{\"cod\":\"200\",\"message\":0,\"cnt\":1,\"list\":[{\"dt\":1629104400,\"main\":{\"temp\":287.77,\"feels_like\":287.22,\"temp_min\":287.77,\"temp_max\":289.43,\"pressure\":1015,\"sea_level\":1015,\"grnd_level\":1013,\"humidity\":74,\"temp_kf\":-1.66},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04d\"}],\"clouds\":{\"all\":70},\"wind\":{\"speed\":5.98,\"deg\":308,\"gust\":9},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2021-08-16 09:00:00\"}],\"city\":{\"id\":2643743,\"name\":\"London\",\"coord\":{\"lat\":51.5085,\"lon\":-0.1257},\"country\":\"GB\",\"population\":1000000,\"timezone\":3600,\"sunrise\":1629089271,\"sunset\":1629141717}}";

            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(cityJson),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var contextMock = new Mock<WeatherContext>();
            contextMock.Setup(a => a.Set<Forecast>()).Returns(Mock.Of<DbSet<Forecast>>);

            var forecastRepositoryMock = new Mock<IRepository<Forecast>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.GetRepository<Forecast>()).Returns(forecastRepositoryMock.Object);

            var optionsMock = Mock.Of<IOptions<WeatherOptions>>(m =>
                       m.Value.ApiKey == TestConstants.ApiKey &&
                       m.Value.ApiUrl == TestConstants.ApiUrl);

            var weatherForecastService = new WeatherForecastService(unitOfWorkMock.Object, mockFactory.Object, optionsMock);
            var controller = new WeatherController(weatherForecastService, optionsMock);
            WeatherForecastRequest request = new WeatherForecastRequest { City = "London", ZipCode = "" };
            //act
            var response = await controller.GetWeatherForecast(request);
            //assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Forecast_Get_By_ZipCode_Succeded()
        {
            //arrange
            var zipCodeJson = "{\"cod\":\"200\",\"message\":0,\"cnt\":1,\"list\":[{\"dt\":1628856000,\"main\":{\"temp\":289.49,\"feels_like\":289.24,\"temp_min\":289.49,\"temp_max\":290.26,\"pressure\":1015,\"sea_level\":1015,\"grnd_level\":1013,\"humidity\":79,\"temp_kf\":-0.77},\"weather\":[{\"id\":801,\"main\":\"Clouds\",\"description\":\"few clouds\",\"icon\":\"02n\"}],\"clouds\":{\"all\":13},\"wind\":{\"speed\":0.68,\"deg\":282,\"gust\":1.13},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2021-08-13 12:00:00\"}],\"city\":{\"id\":0,\"name\":\"Mountain View\",\"coord\":{\"lat\":37.3855,\"lon\":-122.088},\"country\":\"US\",\"population\":0,\"timezone\":-25200,\"sunrise\":1628860994,\"sunset\":1628910195}}";

            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(zipCodeJson),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var contextMock = new Mock<WeatherContext>();
            contextMock.Setup(a => a.Set<Forecast>()).Returns(Mock.Of<DbSet<Forecast>>);

            var forecastRepositoryMock = new Mock<IRepository<Forecast>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.GetRepository<Forecast>()).Returns(forecastRepositoryMock.Object);

            var optionsMock = Mock.Of<IOptions<WeatherOptions>>(m =>
                       m.Value.ApiKey == TestConstants.ApiKey &&
                       m.Value.ApiUrl == TestConstants.ApiUrl);

            var weatherForecastService = new WeatherForecastService(unitOfWorkMock.Object, mockFactory.Object, optionsMock);
            var controller = new WeatherController(weatherForecastService, optionsMock);
            WeatherForecastRequest request = new WeatherForecastRequest { City = "", ZipCode = "94040,us" };
            //act
            var response = await controller.GetWeatherForecast(request);
            //assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Forecast_Get_History_Succeded()
        {
            //arrange
            var mockFactory = new Mock<IHttpClientFactory>();

            var contextMock = new Mock<WeatherContext>();
            contextMock.Setup(a => a.Set<Forecast>()).Returns(Mock.Of<DbSet<Forecast>>);

            var forecastRepositoryMock = new Mock<IRepository<Forecast>>();

            var forecasts = new List<Forecast>() {
                new Forecast {
                    Temperature=new Temperature (45,new Scale { DecimalPlaces=2, InUse=true, ScaleUnit=ScaleUnit.Kelvin }),
                    Humidity=50,
                    WindSpeed=45,
                    Address=new Address("Istanbul","")
                }
            };

            forecastRepositoryMock.Setup(m => m.GetAll()).Returns(forecasts).Verifiable();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.GetRepository<Forecast>()).Returns(forecastRepositoryMock.Object);

            var optionsMock = Mock.Of<IOptions<WeatherOptions>>(m =>
                      m.Value.ApiKey == TestConstants.ApiKey &&
                      m.Value.ApiUrl == TestConstants.ApiUrl);

            var weatherForecastService = new WeatherForecastService(unitOfWorkMock.Object, mockFactory.Object, optionsMock);
            var controller = new WeatherController(weatherForecastService, optionsMock);
            //act
            var response = controller.GetHistory();
            //assert
            Assert.NotNull(response);
        }
    }
}
