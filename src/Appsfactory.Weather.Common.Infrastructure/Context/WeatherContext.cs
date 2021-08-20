using Appsfactory.Weather.Domain.Entities;
using Appsfactory.Weather.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Appsfactory.Weather.Infrastructure.Context
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
        {

        }

        public DbSet<Forecast> Forecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ForecastConfiguration());
        }
    }
}
