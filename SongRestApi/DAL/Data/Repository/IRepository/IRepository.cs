using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SongRestApi.DAL.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<bool> AddItemAsync(T entity);

        Task<T> GetSingleAsync(int id);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null);

        bool Remove(int id);

        bool Remove(T entity);
    }
}
