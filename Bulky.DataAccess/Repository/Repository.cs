using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBookWeb.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db) 
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            //_db.Categories == dbSet 
            
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query.Where(filter);
            }
            
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties
                    .Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries))
                { 
                    query = query.Include(property);
                }
                    
            }

            return  query.ToList();
        }

        public T GetT(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked =false)
        {

            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;   
            }
            else
            {
                query = dbSet.AsNoTracking();               
            }

            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }

            }
            return query.FirstOrDefault();


        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

       
    }
}
