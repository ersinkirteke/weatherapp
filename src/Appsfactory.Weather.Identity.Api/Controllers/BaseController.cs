using Appsfactory.Weather.Identity.Api.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Appsfactory.Weather.Identity.Api.Controllers
{
    public class BaseController<T> : Controller where T : BaseController<T>
    {
        public BaseController()
        {

        }
    }
}
