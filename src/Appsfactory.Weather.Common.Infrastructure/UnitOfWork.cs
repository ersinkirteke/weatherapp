using Appsfactory.Weather.Infrastructure.Context;
using Appsfactory.Weather.Infrastructure.Repositories;
using System.Linq;


namespace Appsfactory.Weather.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WeatherContext _context;

        public UnitOfWork(WeatherContext context)
        {
            _context = context;
        }
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new EFRepository<T>(_context);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            _context
                .ChangeTracker
                .Entries()
                .ToList()
                .ForEach(x => x.Reload());
        }
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
