namespace eshop.Helpers
{
    public class OpinionDto
    {

        public record create(
            int? Id = null,
            string Text = null!,
            bool IsValidate = false,
            bool IsModerate = false,
            int? Client = null,
            int? Product = null
        );

        public record update(
            string Text = null!,
            bool IsValidate = false,
            bool IsModerate = false,
            int? Client = null,
            int? Product = null
        );

        public record find(
            int? Id = null,
            string Text = null!,
            bool IsValidate = false,
            bool IsModerate = false,
            ClientDto.find? Client = null,
            ProductDto.model? Product = null
        );
    }
}
