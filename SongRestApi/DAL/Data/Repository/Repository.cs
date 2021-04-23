using Microsoft.EntityFrameworkCore;
using SongRestApi.DAL.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SongRestApi.DAL.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //old
        //protected readonly DbContext _dbContext;
        //internal DbSet<T> _dbSet;
        //as per microsoft https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
        internal ApplicationDbContext _dbContext;
        internal DbSet<T> _dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<bool> AddItemAsync(T entity)
        {
            if (entity == null)
            {
                //throw new ArgumentException(nameof(entity)); //add this exception when checking bool return of the method (where its called controller)
                return false;
            }

            await _dbSet.AddAsync(entity);
            return true;
        }
           
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        //Use this method when dont need to perform null checks
        public async Task<T> GetSingleAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        //These types of methods (dont return obj) may seem to be return type of void, but is not ideal for trouble shooting (not knowing whats going on)
        public bool Remove(int id)
        {
            T e = _dbSet.Find(id);
            
            if (e == null)
            {
                return false;
            }

            Remove(e);
            
            return true;
        }

        public bool Remove(T entity)
        {
            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            return true;
        }

    }
}
