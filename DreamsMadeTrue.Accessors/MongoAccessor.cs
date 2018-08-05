using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DreamsMadeTrue.Core.Interfaces;
using DreamsMadeTrue.Core.Models;
using MongoDB.Driver;

namespace DreamsMadeTrue.Accessors
{
    public class MongoAccessor<T> : IMongoAccessor<T> where T : IMongoObject
    {
        protected readonly IMongoCollection<T> _collection;
        public MongoAccessor(MongoContext context)
        {
            _collection = context._database.GetCollection<T>(typeof(T).Name);
        }

        public Task<long> Count(Expression<Func<T, bool>> where)
        {
            return _collection.CountDocumentsAsync(where);
        }

        public async Task Delete(Expression<Func<T, bool>> where)
        {
            await _collection.DeleteManyAsync(where);
        }

        public async Task<T> First(Expression<Func<T, bool>> where)
        {
            var results = await _collection.FindAsync(where);
            return results.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> where)
        {
            var result = await _collection.FindAsync(where);
            return result.ToEnumerable();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var result = await _collection.FindAsync(Builders<T>.Filter.Empty);
            return result.ToEnumerable();
        }

        public async Task<T> Insert(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task InsertMany(IEnumerable<T> entities)
        {
            await _collection.InsertManyAsync(entities);
        }

        public Task<T> Update(T entity)
        {
            return _collection.FindOneAndReplaceAsync(e => e.Id == entity.Id, entity);
        }
    }
}
