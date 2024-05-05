namespace eshop.Helpers
{
    public class OrderDetailsDto
    {

        public record find(
            int? Id = null,
            int Quantity = 1,
            decimal Price = 0m,
            ProductDto.model? Product = null,
            OrderDto.find? Order = null
        );

        public record create(
            int? Id = null,
            int Quantity = 1,
            decimal Price = 0m,
            int? Product = null,
            int? Order = null
        );

        public record update(
            int Quantity = 1,
            decimal Price = 0m,
            int? Product = null,
            int? Order = null
        );
    }
}
