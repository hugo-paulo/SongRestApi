using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SongRestApi.DAL.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void AddItem(T entity);

        T GetSingle(int id);

        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        T GetFirstOrDefault(Expression<Func<T, bool>> filter = null);

        bool Remove(int id);

        bool Remove(T entity);
    }
}
