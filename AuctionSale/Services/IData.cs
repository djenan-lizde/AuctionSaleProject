using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuctionSale.Services
{
    public interface IData<T> where T :class
    {
        IEnumerable<T> Get();
        T Get(int id);
        T Get(Expression<Func<T, bool>> predicate, bool flag);
        IEnumerable<T> GetByCondition(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Update(T entity);
        T Update(int id, T entity);
        T Delete(T entity);
        T Delete(int id);
    }
}
