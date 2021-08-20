using Appsfactory.Weather.Identity.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Appsfactory.Weather.Identity.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
