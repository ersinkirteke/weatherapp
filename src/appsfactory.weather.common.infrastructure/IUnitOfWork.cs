using Appsfactory.Weather.Infrastructure.Repositories;
using System;

namespace Appsfactory.Weather.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        void Commit();
        void Rollback();
    }
}
