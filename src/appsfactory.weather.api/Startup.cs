using FluentValidation;
using FluentValidation.AspNetCore;
using Appsfactory.Weather.Api.DTOs;
using Appsfactory.Weather.Api.Extensions;
using Appsfactory.Weather.Api.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Appsfactory.Weather.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Appsfactory.Weather.Api.Services;
using Appsfactory.Weather.Infrastructure;
using Microsoft.AspNetCore.Routing;

namespace Appsfactory.Weather.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation();
            services.AddDbContext<WeatherContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DBConnection"))
            );
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IWeatherForecastService, WeatherForecastService>();
            services.AddTransient<IValidator<AddressDto>, AddressValidator>();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "appsfactory.weather.api", Version = "v1" });
            });
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddHttpClient();
            services.ConfigureCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WeatherContext weatherContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "appsfactory.weather.api v1"));
            }

            weatherContext.Database.EnsureCreated();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ConfigureCustomExceptionMiddleware();
        }
    }
}
