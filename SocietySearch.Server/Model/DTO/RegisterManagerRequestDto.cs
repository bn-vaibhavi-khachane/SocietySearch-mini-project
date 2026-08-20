using System.ComponentModel.DataAnnotations;

namespace SocietySearch.Server.Model.DTO
{
    public class RegisterManagerRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
