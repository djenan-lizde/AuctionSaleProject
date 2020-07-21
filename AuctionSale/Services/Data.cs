using AuctionSale.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuctionSale.Services
{
    public class Data<T> : IData<T> where T : class, IEntity
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> entities;

        public Data(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }

        public T Add(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Entity");
            }

            var x = entities.Add(obj);
            _context.SaveChanges();
            return x.Entity;
        }
        public IEnumerable<T> Get()
        {
            return entities.AsNoTracking().AsEnumerable();
        }
        public T Get(int id)
        {
            return entities.FirstOrDefault(x => x.Id == id);
        }
        public T Get(Expression<Func<T, bool>> predicate, bool flag)
        {
            if (flag)
                return entities.AsNoTracking().FirstOrDefault(predicate);
            else
                return entities.AsNoTracking().LastOrDefault(predicate);
        }
        public T Update(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Entity");
            }

            _context.Attach(obj).State = EntityState.Modified;
            _context.SaveChanges();
            return obj;
        }
        public T Update(int id, T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return entity;
        }
        public T Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity");
            }

            entities.Remove(entity);
            _context.SaveChanges();
            return entity;
        }
        public T Delete(int id)
        {
            var obj = Get(id);
            entities.Remove(obj);
            _context.SaveChanges();
            return obj;
        }
        public IEnumerable<T> GetByCondition(Expression<Func<T, bool>> predicate)
        {
            return entities.AsNoTracking().Where(predicate);
        }
    }
}
