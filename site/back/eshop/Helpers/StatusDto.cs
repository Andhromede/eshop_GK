namespace eshop.Helpers
{
    public class StatusDto
    {
        public record model(
            int? Id = null,
            string Name = null!,
            bool IsActive = true
      );
    }
}
