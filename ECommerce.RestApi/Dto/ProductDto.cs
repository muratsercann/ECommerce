namespace ECommerce.RestApi.Dto
{
    public record ProductDto
    {
        public string Id { get; init; }
        public string CategoryId { get; init; }
        public string Name { get; init; }
        public int Rating { get; init; }
        public int Stock { get; init; }
        public decimal Price { get; init; }
        public string Description { get; init; } = "";
        public string ImageUrl { get; init; } = "";
    }

    public record AddProductDto
    {
        public string Name { get; init; }
        public int Rating { get; init; }
        public int Stock { get; init; }
        public decimal Price { get; init; }
        public string Description { get; init; } = "";
        public string ImageUrl { get; init; } = "";
    }

}
