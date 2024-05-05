using eshop.Models;

namespace eshop.Helpers
{
    public class ClientDto
    {
        public record create(
           int? Id = null,
           string Email = null!,
           string Password = null!,
           string? Firstname = null,
           string? Lastname = null,
           string? Tel = null,
           string? Adress = null,
           int? Cp = null,
           string? City = null,
           string? Country = null,
           bool IsActive = true,
           RoleDto.model? Role = null
       );

        public record update(
           string Email = null!,
           string Password = null!,
           string? Firstname = null,
           string? Lastname = null,
           string? Tel = null,
           string? Adress = null,
           int? Cp = null,
           string? City = null,
           string? Country = null,
           bool IsActive = true,
           int? Role = null
       );

        public record find(
           int? Id = null,
           string Email = null!,
           string? Firstname = null,
           string? Lastname = null,
           string? Tel = null,
           string? Adress = null,
           int? Cp = null,
           string? City = null,
           string? Country = null,
           bool IsActive = true,
           RoleDto.model? Role = null
       );

        public record login(
            string Email = null,
            string Password = null
        );

        public record register(
            string Email = null,
            string Password = null,
            string ConfirmPassword = null
        );

    }
}
