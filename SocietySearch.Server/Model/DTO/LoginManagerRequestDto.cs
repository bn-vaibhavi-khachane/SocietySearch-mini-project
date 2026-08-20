using System.ComponentModel.DataAnnotations;

namespace SocietySearch.Server.Model.DTO
{
    public class LoginManagerRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
