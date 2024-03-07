using ECommerce.RestApi.Models;
using System.Linq.Expressions;

namespace ECommerce.RestApi.Dto
{
    public record CreateCategoryDto(
        string Name,
        string ParentId = "",
        string Description = "");

    public record CategoryDto
    {
        public string Id { get; init; }
        public string Name { get; init; }

        public static Expression<Func<Category, CategoryDto>> Selector
        {
            get
            {
                return category => new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name
                };

            }
        }
    }

}
