using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Appsfactory.Weather.Api.Controllers
{
    public class BaseController<T> : Controller where T : BaseController<T>
    {
        private IConfiguration _configuration;

        protected IConfiguration Configuration => _configuration ?? (_configuration = HttpContext?.RequestServices.GetService<IConfiguration>());

    }
}
