using System.ComponentModel.DataAnnotations;

namespace SocietySearch.Server.Model.DTO
{
    public class AddSocietyRequestDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string Summary { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Required, RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "EstablishmentYear must be a valid four-digit year.")]
        public string EstablishmentYear { get; set; } = string.Empty;

        [Required, Url]
        public string SocietyImageUrl { get; set; } = string.Empty;

        // Ids of amenities associated with this society, stored as JSON
        public List<Guid?> AmenityIds { get; set; } = new();
    }
}
