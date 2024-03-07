using AutoMapper;
using ECommerce.RestApi.Models;
using MongoDB.Driver;

namespace ECommerce.RestApi.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository 
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        public CategoryRepository(ECommerceContext mongoContext) : base(mongoContext)
        {
            _categoryCollection = mongoContext.Categories;
        }
    }



}
