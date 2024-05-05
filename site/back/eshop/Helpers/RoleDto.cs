using eshop.Models;

namespace eshop.Helpers
{
    public class RoleDto
    {
        public record model(
            int? Id = null,
            string Name = null!,
            bool IsActive = true
        );
        
    }
}
