using Appsfactory.Weather.Infrastructure.Specifications;
using System.Collections.Generic;

namespace Appsfactory.Weather.Infrastructure.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IReadOnlyCollection<TEntity> GetAll();
        IReadOnlyCollection<TEntity> Get(ISpecification<TEntity> spec);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(int id);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
