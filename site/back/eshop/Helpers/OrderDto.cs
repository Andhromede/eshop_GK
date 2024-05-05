namespace eshop.Helpers
{
    public class OrderDto
    {
        public record find(
            int? Id = null,
            DateTime? OrderDate = null!,
            DateTime? ValidationDate = null,
            DateTime? ShippingDate = null,
            ClientDto.find? Client = null,
            StatusDto.model? Status = null
        );

        public record create(
            int? Id = null,
            DateTime? OrderDate = null!,
            DateTime? ValidationDate = null,
            DateTime? ShippingDate = null,
            int? Client = null,
            int? Status = null
        );

        public record update(
            DateTime? OrderDate = null!,
            DateTime? ValidationDate = null,
            DateTime? ShippingDate = null,
            int? Client = null,
            int? Status = null
        );


        /*public record Find
        {
            public int? Id { get; init; }
            public DateTime? OrderDate { get; init; }
            public DateTime? ValidationDate { get; init; }
            public DateTime? ShippingDate { get; init; }
            public ClientDto.Find? Client { get; init; }
            public StatusDto.Model? Status { get; init; }
            public List<OrderDetailsDto> OrderDetails { get; init; }

            public Find(
                int? id = null,
                DateTime? orderDate = null,
                DateTime? validationDate = null,
                DateTime? shippingDate = null,
                ClientDto.Find? client = null,
                StatusDto.Model? status = null,
                List<OrderDetailsDto>? orderDetails = null)
            {
                Id = id;
                OrderDate = orderDate;
                ValidationDate = validationDate;
                ShippingDate = shippingDate;
                Client = client;
                Status = status;
                OrderDetails = orderDetails ?? new List<OrderDetailsDto>();
            }
        }*/

    }
}
