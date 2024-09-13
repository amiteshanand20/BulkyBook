using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> Dbset;

        public Repository(ApplicationDbContext _db)
        {
            _context= _db;
            _db.Products.Include(x => x.Category).Include(x => x.CoverType);
            this.Dbset= _db.Set<T>();
        }
        public void Add(T entity)
        {
            Dbset.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<T> query = Dbset;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.ToList();
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null,bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = Dbset;
            }
            else
            {
                query = Dbset.AsNoTracking(); //explicitly stating efcore to not track changes 
            }
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var property in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            Dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Dbset.RemoveRange(entities);
        }
    } 
}
