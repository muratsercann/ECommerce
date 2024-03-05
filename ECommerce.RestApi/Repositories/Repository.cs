
using ECommerce.RestApi.Models.DTOs;
using ECommerce.RestApi.Models;
using MongoDB.Driver;
using System.Linq.Expressions;
using SharpCompress.Common;

namespace ECommerce.RestApi.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ECommerceContext _mongoContext;
        private readonly IMongoCollection<TEntity> _collection;

        public Repository(ECommerceContext mongoContext)
        {
            _mongoContext = mongoContext;
            _collection = _mongoContext._database.GetCollection<TEntity>(nameof(TEntity).ToLowerInvariant());
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return true;
        }

        public async Task<bool> AddAsync(List<TEntity> entities)
        {
            await _collection.InsertManyAsync(entities);
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);
            await _collection.DeleteOneAsync(filter);
            return true;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);
            var result = await _collection.Find(filter).AnyAsync();
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var filter = Builders<TEntity>.Filter.Empty;
            var result = await _collection.Find(filter).ToListAsync();
            return result;
        }

        public async Task<TResult> GetByIdAsync<TResult>(string id, Expression<Func<TEntity, TResult>> selector)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);

            var result = await _collection
              .Find(filter)
              .Project(selector).FirstOrDefaultAsync();

            return result;
        }

        public async Task<TResult> GetByIdAsync<TResult>(IEnumerable<string> ids, Expression<Func<TEntity, TResult>> selector)
        {
            var filter = Builders<TEntity>.Filter.In("_id", ids);

            var result = await _collection
              .Find(filter)
              .Project(selector).FirstOrDefaultAsync();

            return result;
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);

            var result = await _collection
              .Find(filter).FirstOrDefaultAsync();

            return result;
        }

        public async Task<long> GetCountAsync()
        {
            var filter = Builders<TEntity>.Filter.Empty;
            var result = await _collection.CountDocumentsAsync(filter);
            return result;
        }
    }
}
