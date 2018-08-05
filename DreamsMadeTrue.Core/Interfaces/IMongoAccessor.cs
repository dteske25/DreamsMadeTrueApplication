using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DreamsMadeTrue.Core.Models;

namespace DreamsMadeTrue.Core.Interfaces
{
    public interface IMongoAccessor<T> where T : IMongoObject
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> where);
        Task<T> First(Expression<Func<T, bool>> where);
        Task<long> Count(Expression<Func<T, bool>> where);
        Task Delete(Expression<Func<T, bool>> where);
        Task<T> Insert(T entity);
        Task InsertMany(IEnumerable<T> entities);
        Task<T> Update(T entity);
    }
}
