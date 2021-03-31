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

        //public Repository(DbContext dbContext)
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        //?change this to bool?
        public void AddItem(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException(nameof(entity));
            }

            _dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.FirstOrDefault();
        }

        //Use this method when dont need to perform null checks
        public T GetSingle(int id)
        {
            return _dbSet.Find(id);
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
