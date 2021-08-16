using Appsfactory.Weather.Api.Controllers;
using Appsfactory.Weather.Api.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Appsfactory.Weather.Api.Test
{
    public class WeatherControllerTest
    {
        private readonly Mock<IWeatherForecastService> _weatherServiceMock;
        public WeatherControllerTest()
        {
            _weatherServiceMock = new Mock<IWeatherForecastService>();
        }

        [Fact]
        public async Task Forecast_By_City_Succeded()
        {
            //arrange
            var controller = new WeatherController(_weatherServiceMock.Object);
            //act
            var response = controller.GetWeatherForecast("London", null);
            //assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Forecast_By_ZipCode_Succeded()
        {
            //arrange
            var controller = new WeatherController(_weatherServiceMock.Object);
            //act
            var response = controller.GetWeatherForecast(null, "94040,us");
            //assert
            Assert.NotNull(response);
        }
    }
}
