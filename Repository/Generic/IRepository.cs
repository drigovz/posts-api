using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PostsApi.Repository.Generic
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}