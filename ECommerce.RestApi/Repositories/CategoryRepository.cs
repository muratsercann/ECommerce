using AutoMapper;
using ECommerce.RestApi.Models;

namespace ECommerce.RestApi.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository 
    {
        private readonly ECommerceContext _mongoContext;

        public CategoryRepository(ECommerceContext mongoContext) : base(mongoContext)
        {
            _mongoContext = mongoContext;
        }
    }



}
