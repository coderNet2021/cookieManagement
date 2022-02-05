using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lodgify.Repository.IRepository
{
    public interface IRepositoryBase<T>
    {
        Task<T> Find(int id);

        Task<IEnumerable<T>> FindAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
            bool isTracking = true
            );

        Task<T> FirstOrDefault(
            Expression<Func<T, bool>> filter = null,
             Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
            bool isTracking = true
            );

        Task Add(T entity);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);

        void Update(T entity);

        Task Save();
    }
}
