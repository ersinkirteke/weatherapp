using Appsfactory.Weather.Domain.Entities;
using Appsfactory.Weather.Infrastructure.Context;

namespace Appsfactory.Weather.Infrastructure.Repositories
{
    public class ForecastRepository : EFRepository<Forecast>
    {
        public ForecastRepository(WeatherContext dbContext) : base(dbContext)
        {

        }
    }
}
