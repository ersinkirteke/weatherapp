using Appsfactory.Weather.Api.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Appsfactory.Weather.Api.Controllers
{
    public class BaseController<T> : Controller where T : BaseController<T>
    {
        private readonly WeatherOptions _options;
        public BaseController(IOptions<WeatherOptions> options)
        {
            _options = options.Value;
        }

    }
}
