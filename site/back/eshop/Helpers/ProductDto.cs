namespace eshop.Helpers
{
    public class ProductDto
    {
        public record model(
            int? Id = null,
            string Name = null!,
            string Description = null!,
            decimal? Height = null,
            decimal? Width = null,
            decimal? Length = null,
            decimal? Weight = null,
            int? Capacity = null,
            decimal? Price = null,
            string? Maker = null,
            string? Color = null,
            string? Image = null,
            bool IsActive = true
        );

        public record post(
            int? Id = null,
            string Name = null!,
            string Description = null!,
            decimal? Height = null,
            decimal? Width = null,
            decimal? Length = null,
            decimal? Weight = null,
            int? Capacity = null,
            decimal? Price = null,
            string? Maker = null,
            string? Color = null,
            IFormFile Image = null,
            bool IsActive = true
        );

        public record get(
            int? Id = null,
            string Name = null!,
            string Description = null!,
            decimal? Height = null,
            decimal? Width = null,
            decimal? Length = null,
            decimal? Weight = null,
            int? Capacity = null,
            decimal? Price = null,
            string? Maker = null,
            string? Color = null,
            string? Image = null,
            bool IsActive = true
        );

        public record put(
            string Name = null!,
            string Description = null!,
            decimal? Height = null,
            decimal? Width = null,
            decimal? Length = null,
            decimal? Weight = null,
            int? Capacity = null,
            decimal? Price = null,
            string? Maker = null,
            string? Color = null,
            IFormFile Image = null,
            bool IsActive = true
        );

    }
}
