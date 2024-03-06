using AutoMapper;
using ECommerce.RestApi.Models;
using MongoDB.Driver;

namespace ECommerce.RestApi.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository 
    {
        private readonly ECommerceContext _mongoContext;
        private readonly IMongoCollection<Category> _collection;

        public CategoryRepository(ECommerceContext mongoContext) : base(mongoContext)
        {
            _mongoContext = mongoContext;
            _collection = _mongoContext.GetCollection<Category>();
        }
    }



}
